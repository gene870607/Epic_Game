var searchbar = document.querySelector('.navbar .form-control');
var navBar = document.querySelector('.navbar');
var searchnav = document.querySelector('svg');
var onoff = false;
var li = document.querySelector('.div_itemlist li');
console.log(searchbar);
searchnav.addEventListener('click', function () {
    if (onoff == false) {
        searchbar.setAttribute('style', 'display: block');
        navBar.setAttribute('style', 'width: 100%');
        onoff = true;
    }
    else if (onoff == true) {
        searchbar.setAttribute('style', 'display: none');
        navBar.setAttribute('style', 'width: initial');

        onoff = false;
    }
});
    
    
// ===================================一下篩選器====================================

// mtree.js
// Requires jquery.js and velocity.js (optional but recommended).
// Copy the below function, add to your JS, and simply add a list <ul class=mtree> ... </ul>
; (function ($, window, document, undefined) {

    // Only apply if mtree list exists
    if ($('ul.mtree').length) {


        // Settings
        var collapsed = true; // Start with collapsed menu (only level 1 items visible)
        var close_same_level = false; // Close elements on same level when opening new node.
        var duration = 400; // Animation duration should be tweaked according to easing.
        var listAnim = true; // Animate separate list items on open/close element (velocity.js only).
        var easing = 'easeOutQuart'; // Velocity.js only, defaults to 'swing' with jquery animation.


        // Set initial styles 
        $('.mtree ul').css({ 'overflow': 'hidden', 'height': (collapsed) ? 0 : 'auto', 'display': (collapsed) ? 'none' : 'block' });

        // Get node elements, and add classes for styling
        var node = $('.mtree li:has(ul)');
        node.each(function (index, val) {
            $(this).children(':first-child').css('cursor', 'pointer')
            $(this).addClass('mtree-node mtree-' + ((collapsed) ? 'closed' : 'open'));
            $(this).children('ul').addClass('mtree-level-' + ($(this).parentsUntil($('ul.mtree'), 'ul').length + 1));
        });

        // Set mtree-active class on list items for last opened element
        $('.mtree li > *:first-child').on('click.mtree-active', function (e) {
            if ($(this).parent().hasClass('mtree-closed')) {
                $('.mtree-active').not($(this).parent()).removeClass('mtree-active');
                $(this).parent().addClass('mtree-active');
            } else if ($(this).parent().hasClass('mtree-open')) {
                $(this).parent().removeClass('mtree-active');
            } else {
                $('.mtree-active').not($(this).parent()).removeClass('mtree-active');
                $(this).parent().toggleClass('mtree-active');
            }
        });

        // Set node click elements, preferably <a> but node links can be <span> also
        node.children(':first-child').on('click.mtree', function (e) {

            // element vars
            var el = $(this).parent().children('ul').first();
            var isOpen = $(this).parent().hasClass('mtree-open');

            // close other elements on same level if opening 
            if ((close_same_level || $('.csl').hasClass('active')) && !isOpen) {
                var close_items = $(this).closest('ul').children('.mtree-open').not($(this).parent()).children('ul');

                // Velocity.js
                if ($.Velocity) {
                    close_items.velocity({
                        height: 0
                    }, {
                        duration: duration,
                        easing: easing,
                        display: 'none',
                        delay: 100,
                        complete: function () {
                            setNodeClass($(this).parent(), true)
                        }
                    });

                    // jQuery fallback
                } else {
                    close_items.delay(100).slideToggle(duration, function () {
                        setNodeClass($(this).parent(), true);
                    });
                }
            }

            // force auto height of element so actual height can be extracted
            el.css({ 'height': 'auto' });

            // listAnim: animate child elements when opening
            if (!isOpen && $.Velocity && listAnim) el.find(' > li, li.mtree-open > ul > li').css({ 'opacity': 0 }).velocity('stop').velocity('list');

            // Velocity.js animate element
            if ($.Velocity) {
                el.velocity('stop').velocity({
                    //translateZ: 0, // optional hardware-acceleration is automatic on mobile
                    height: isOpen ? [0, el.outerHeight()] : [el.outerHeight(), 0]
                }, {
                    queue: false,
                    duration: duration,
                    easing: easing,
                    display: isOpen ? 'none' : 'block',
                    begin: setNodeClass($(this).parent(), isOpen),
                    complete: function () {
                        if (!isOpen) $(this).css('height', 'auto');
                    }
                });

                // jQuery fallback animate element
            } else {
                setNodeClass($(this).parent(), isOpen);
                el.slideToggle(duration);
            }

            // We can't have nodes as links unfortunately
            e.preventDefault();
        });

        // Function for updating node class
        function setNodeClass(el, isOpen) {
            if (isOpen) {
                el.removeClass('mtree-open').addClass('mtree-closed');
            } else {
                el.removeClass('mtree-closed').addClass('mtree-open');
            }
        }

        // List animation sequence
        if ($.Velocity && listAnim) {
            $.Velocity.Sequences.list = function (element, options, index, size) {
                $.Velocity.animate(element, {
                    opacity: [1, 0],
                    translateY: [0, -(index + 1)]
                }, {
                    delay: index * (duration / size / 2),
                    duration: duration,
                    easing: easing
                });
            };
        }

        // Fade in mtree after classes are added.
        // Useful if you have set collapsed = true or applied styles that change the structure so the menu doesn't jump between states after the function executes.
        if ($('.mtree').css('opacity') == 0) {
            if ($.Velocity) {
                $('.mtree').css('opacity', 1).children().css('opacity', 0).velocity('list');
            } else {
                $('.mtree').show(200);
            }
        }
    }
}(jQuery, this, this.document));


/* DEMO STUFF */
$(document).ready(function () {
    var mtree = $('ul.mtree');

    // Skin selector for demo
    mtree.wrap('<div class=mtree-demo></div>');
    var skins = ['bubba', 'skinny', 'transit', 'jet', 'nix'];
    mtree.addClass(skins[0]);

});

// ===================================一上篩選器====================================



// ===================================以下排序功能=================================

let label_checkbox = document.querySelectorAll("#checkbox");

document.querySelector("#OrderByDate").addEventListener('click', () => {
    OrderBy("ReleaseDate", true);
});

document.querySelector("#OrderByAlphabetical").addEventListener('click', () => {
    OrderBy("ProductName", true);
});

label_checkbox.forEach(element => {
    element.addEventListener('click', () => {
        OrderBy("ReleaseDate", true);
    })
})

function getTotal() {
    let label_checkbox = document.querySelectorAll("#checkbox");
    let total = 0;
    label_checkbox.forEach(element => {
        if (element.checked == true) {
            total += parseInt(element.value);
        }
    })
    return total;
}   



function OrderBy(_Key, _bool) {

    let total = getTotal();
    $.ajax({
        url: '/Home/SearchOrder',
        data: { Key: `${_Key}`, Boo: _bool, num: total },
        type: 'post',
        success: function (json) {
            let ul = $('#search_ul');
            ul.empty();
            for (let i = 0; i < json.length; i++) {
                let li = $('<li></li>').attr('class', 'col-6 col-md-3 mode_4_li');
                let a = $('<a></a>').attr('href', '/Product/' + json[i].ProductID);
                let div = $('<div></div>').attr('class', 'mode_4_img-2');
                let div1 = $('<div></div>').attr('class', 'imgArea').attr('style', 'background-image: url(' + json[i].Url + ')');
                let div2 = $('<div></div>').attr('class', 'mode_4_text');

                let h4 = $('<h4></h4>').html(json[i].ProductName);
                let p = $('<p></p>').html(json[i].Developer + '|' + json[i].Publisher);
                let h41 = $('<h4></h4>').html('TWD$' + json[i].Price);

                li.append(a);
                a.append(div).append(div2);
                div.append(div1);
                div2.append(h4).append(p).append(h41);
                ul.append(li);
            }
        },
        error: function () {
        }
    })
}

//====================================以上排序功能==================================

//====================================以下收尋功能==================================

var search = document.querySelector(".searchArea")
search.addEventListener('keydown', function (event) {

    if (event.keyCode == 13) {
        let userinput = search.value;
        let h = "/Home/Search/" + userinput;
        window.location.href = h;
    }
});

//====================================以上收尋功能==================================