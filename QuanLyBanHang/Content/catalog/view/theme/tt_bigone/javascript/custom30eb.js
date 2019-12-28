$(document).ready(function () {
    /*header menu*/
    if ($('body').hasClass('common-home')) {
        $('#pt_menu_home').addClass('act');
    }

    /*header vertical menu*/
    $('.vermagemenu h2').click(function () {
        $(".navleft-container").slideToggle();
    });

    // hide #back-top first
    $("#back-top").hide();
    // fade in #back-top
    $(function () {
        $(window).scroll(function () {
            if ($(this).scrollTop() > 300) {
                $('#back-top').fadeIn();
            } else {
                $('#back-top').fadeOut();
            }
        });
        // scroll body to 0px on click
        $('#back-top').click(function () {
            $('body,html').animate({scrollTop: 0}, 800);
            return false;
        });
    });

    /*search*/
    var flag = false;
    var current_cate_value = $('select.cate-items option.selected').data('value');
    var current_cate_text = $('select.cate-items option.selected').html();
    $('.cate-selected').attr('data-value', current_cate_value);
    $('.cate-selected').html(current_cate_text);
    $('.hover-cate p').click(function () {
        $(".cate-items").toggle("slow");
    });
    $('.hover-cate').hover(
        function () {
            flag = true;
        },
        function () {
            flag = false;
        }
    );
    $('ul.cate-items li.item-cate').click(function () {
        var cate_search = $(this).data('value');
        var text_search = $('#text-search').val();
        $('.cate-selected').attr('data-value', cate_search);
        $('.cate-selected').html($(this).html());
        $(".cate-items").hide();
        $('#text-search').focus();
    });
    $('#btn-search-category').click(function () {
        var url = $('base').attr('href') + '/product/search';
        var text_search = $('#text-search').val();
        if (text_search) {
            url += '?search=' + encodeURIComponent(text_search);
        }

        var category_search = $('.cate-selected').attr("data-value");
        if (category_search) {
            url += '&category_id=' + encodeURIComponent(category_search);
        }

        location = url;
    });
    /*end search*/
});