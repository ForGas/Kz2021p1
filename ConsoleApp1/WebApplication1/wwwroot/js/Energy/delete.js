$(document).ready(function () {
    $('#b-delete').click(function () {
        var clicked = $(this);
        clicked.attr('disabled', 'disabled');

        var id = clicked.attr('personal-id');
        var url = '/PersonalAccount/Delete?id=' + id;

        $.post(url).done(function (answer) {
            if (answer) {
                console.log(answer);

                $('.content').remove();

                $('.section-main').append('<h1 class="fs-5 text-success text-center">Удаление произошло успешно</h1>');
            }
            else {
                $('.content').remove();

                $('.section-main').append('<h1 class="fs-5 text-danger text-center">Удаление не произошло</h1>');
            }
        });
    });
});