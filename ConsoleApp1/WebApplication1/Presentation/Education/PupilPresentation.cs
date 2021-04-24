﻿using AutoMapper;
using Microsoft.AspNetCore.Routing;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.Models;

namespace WebApplication1.Presentation
{
    public class PupilPresentation
    {
        private PupilRepository _pupilRepository;
        private StudentRepository _studentRepository;
        private IMapper Mapper { get; set; }

        public PupilPresentation(PupilRepository pupilRepository, StudentRepository studentRepository, IMapper mapper)
        {
            _pupilRepository = pupilRepository;
            _studentRepository = studentRepository;
            Mapper = mapper;
        }

        public PagingList<PupilViewModel> GetPupilList(int page)
        {
            var pupils = _pupilRepository
                  .GetAll()
                  .Select(x => Mapper.Map<PupilViewModel>(x))
                  .ToList();
            var model = PagingList.Create(pupils, 3, page);
            model.Action = "PupilListAndSearch";
            return model;
        }
        public PagingList<PupilViewModel> GetPupilListAndSearch(string searchBy, string searchPupil, int page)
        {
            var query = from x in _pupilRepository.GetAll() select x;
            if (!String.IsNullOrEmpty(searchPupil))
            {
                if (searchBy == "iin")
                {
                    query = query.Where(x => x.IIN.Equals(searchPupil));
                }
                else if (searchBy == "name")
                {
                    query = query.Where(x => x.Name.Contains(searchPupil));
                }
                else if (searchBy == "classYear")
                {
                    query = query.Where(x => x.ClassYear == int.Parse(searchPupil)).OrderBy(s => s.SchoolId);
                }
                else if (searchBy == "schoolID")
                {
                    query = query.Where(x => x.SchoolId == int.Parse(searchPupil)).OrderBy(s => s.ClassYear);
                }
            }
            List<PupilViewModel> pupilViewModels = new List<PupilViewModel>();
            foreach (var item in query)
            {
                pupilViewModels.Add(Mapper.Map<PupilViewModel>(item));
            }
            var model = PagingList.Create(pupilViewModels, 3, page);
            model.RouteValue = new RouteValueDictionary {
                                    {"searchBy", searchBy},
                                    {"searchPupil", searchPupil} };

            model.Action = "PupilListAndSearch";
            return model;
        }

        public PupilViewModel GetPupilById(long pupilId)
        {
            var pupil = _pupilRepository.Get(pupilId);
            var pupilViewModel = Mapper.Map<PupilViewModel>(pupil);
            return pupilViewModel;
        }

        public void GetAddNewOrEditPupil(PupilViewModel pupilViewModel)
        {
            var pupil = Mapper.Map<Pupil>(pupilViewModel);

            _pupilRepository.Save(pupil);
        }
        /*
                public void RemovePupil(PupilViewModel pupilViewModel)
                {
                    var pupil = Mapper.Map<Pupil>(pupilViewModel);
                    _pupilRepository.Remove(pupil);
                }*/

        public void GetPupilGrant(int minValueForGrant)
        {
            var allFaculties = _studentRepository.GetAllFaculties();
            Random rand = new Random();

            var pupils = _pupilRepository.GetAll();
            foreach (var pupil in pupils)
            {
                if (pupil.ENT != null)
                {
                    StudentViewModel studentVIewModel = new StudentViewModel();
                    studentVIewModel.IIN = pupil.IIN;
                    studentVIewModel.Name = pupil.Name;
                    studentVIewModel.Surname = pupil.Surname;
                    studentVIewModel.Patronymic = pupil.Patronymic;
                    studentVIewModel.Avatar = pupil.Avatar;
                    studentVIewModel.Birthday = pupil.Birthday;
                    studentVIewModel.Email = pupil.Email;
                    studentVIewModel.Faculty = allFaculties.Where(x => x.GetHashCode() == rand.Next(0, allFaculties.Count())).SingleOrDefault().ToString();
                    studentVIewModel.CourseYear = 1;
                    studentVIewModel.Gpa = 2.67;
                    studentVIewModel.EnteredYear = DateTime.Now;
                    studentVIewModel.GraduatedYear = null;
                    studentVIewModel.UniversityId = rand.Next(100, 101); // Random()

                    if (pupil.ENT >= minValueForGrant)
                    {
                        studentVIewModel.OnGrant = true;
                        var student = Mapper.Map<Student>(studentVIewModel);
                        _studentRepository.Save(student);

                        _pupilRepository.Remove(pupil);
                    }
                    else
                    {
                        studentVIewModel.OnGrant = false;
                        var student = Mapper.Map<Student>(studentVIewModel);
                        _studentRepository.Save(student);

                        _pupilRepository.Remove(pupil);
                    }
                }
            }
        }

        public void EndStudyYearForSchool()
        {
            List<Pupil> pupils = _pupilRepository.GetAll();
            Random rand = new Random();
            foreach (Pupil pupil in pupils)
            {
                if (pupil.ClassYear != 11)
                {
                    pupil.ClassYear = pupil.ClassYear + 1;
                }
                else
                {
                    pupil.ENT = rand.Next(50, 140);
                    pupil.GraduatedYear = DateTime.Now;
                    // Certificate
                }
                _pupilRepository.Save(pupil);
            }
        }
    }
}
