
  (function ($) {
  
  "use strict";

    // MENU

    $('.smoothscroll').click(function(){
      var el = $(this).attr('href');
      var elWrapped = $(el);
      var header_height = $('.navbar').height();
  
      scrollToDiv(elWrapped,header_height);
      return false;
  
      function scrollToDiv(element,navheight){
        var offset = element.offset();
        var offsetTop = offset.top;
        var totalScroll = offsetTop-navheight;
  
        $('body,html').animate({
        scrollTop: totalScroll
        }, 300);
      }
    });

    $(window).on('scroll', function(){
      function isScrollIntoView(elem, index) {
        var docViewTop = $(window).scrollTop();
        var docViewBottom = docViewTop + $(window).height();
        var elemTop = $(elem).offset().top;
        var elemBottom = elemTop + $(window).height()*.5;
        if(elemBottom <= docViewBottom && elemTop >= docViewTop) {
          $(elem).addClass('active');
        }
        if(!(elemBottom <= docViewBottom)) {
          $(elem).removeClass('active');
        }
        var MainTimelineContainer = $('#vertical-scrollable-timeline')[0];
        var MainTimelineContainerBottom = MainTimelineContainer.getBoundingClientRect().bottom - $(window).height()*.5;
        $(MainTimelineContainer).find('.inner').css('height',MainTimelineContainerBottom+'px');
      }
      var timeline = $('#vertical-scrollable-timeline li');
      Array.from(timeline).forEach(isScrollIntoView);
    });
  
  })(window.jQuery);
  

  function showRegister() {
    document.getElementById('login-container').style.display = 'none';
    document.getElementById('comp-container').style.display = 'none';
    document.getElementById('register-container').style.display = 'block';
  }
  
  function showLogin() {
    document.getElementById('register-container').style.display = 'none';
    document.getElementById('comp-container').style.display = 'none';
    document.getElementById('login-container').style.display = 'block';
  }
  function showComp() {
    document.getElementById('register-container').style.display = 'none';
    document.getElementById('login-container').style.display = 'none';
    document.getElementById('comp-container').style.display = 'block';
  }
  
  function login() {
    alert('Saljemo upit za logovanje');
  }
  
  function register() {
    alert('Saljemo upit za registrovanje');
  }
  function comp() {
    alert('Saljemo upit za kompanije');
  }
  


