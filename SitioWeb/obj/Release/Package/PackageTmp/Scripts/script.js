$(document).ready(function(){

    $('.cover-text-box').waypoint(function (direction) {
        if(direction == "down"){
            $('nav').addClass('sticky');
        }else{
            $('nav').removeClass('sticky');
        }
    },{
        offset:'53px;'
    });

   /*smooth scrolling*/

   $('a[href*="#"]')
  .not('[href="#"]')
  .not('[href="#0"]')
  .click(function(event) {
    if (
      location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') 
      && 
      location.hostname == this.hostname
    ) {
      var target = $(this.hash);
      target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
      if (target.length) {
        event.preventDefault();
        $('html, body').animate({
          scrollTop: target.offset().top
        }, 1000, function() {
          var $target = $(target);
          $target.focus();
          if ($target.is(":focus")) {
            return false;
          } else {
            $target.attr('tabindex','-1');
            $target.focus(); 
          };
        });
      }
    }
  });

  /* Animations on scroll */

  $('.js--wp-1').waypoint(function(direction){
      $('.js--wp-1').addClass('animated fadeIn');
  },{
    offset:'50%'
  })

  $('.js--wp-2').waypoint(function(direction){
      $('.js--wp-2').addClass('animated fadeInUpBig');
  },{
    offset:'100%'
  })

  $('.js--wp-3').waypoint(function(direction){
      $('.js--wp-3').addClass('animated fadeIn');
  },{
    offset:'80%'
  })

  $('.js--wp-4').waypoint(function(direction){
      $('.js--wp-4').addClass('animated pulse');
  },{
    offset:'50%'
  })


  /*mobile nav*/
  $('.js--nav-icon').click(function(){
    
    var nav = $('.js--main-nav');
    var icon = $('.js--nav-icon i');

    nav.slideToggle(200);

    if(icon.hasClass('ion-md-menu')){
      icon.addClass('ion-md-close');
      icon.removeClass('ion-md-menu');
    }else{
      icon.addClass('ion-md-menu');
      icon.removeClass('ion-md-close');
    }

  })

});

function MasInformacion(id) {
    if ($('#MasInformacion' + id).is(':visible'))
        $('#MasInformacion' + id).hide(1000);
    else
        $('#MasInformacion' + id).show("slow");
}