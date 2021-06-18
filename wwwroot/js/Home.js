$(document).ready(function () {
   $('.food').mouseover(function (){
       $('.content').css('background','url(../images/back2.jpg) center/cover no-repeat');
       //slideAnimate ();
   })
    $('.food').mouseout(function (){
        $('.content').css('background','url(../images/background.jpg) center/cover no-repeat');
    })
    $('.shop').mouseover(function (){
        $('.content').css('background','url(../images/clothes.jpg) center/cover no-repeat');
        //slideAnimate ();
    })
    $('.shop').mouseout(function (){
        $('.content').css('background','url(../images/background.jpg) center/cover no-repeat');
    })
   
   function slideAnimate (){
       $('.slide').animate({
           start:function (){
               $('.slide').removeClass('hide');
           },
           done:function (){
               $('.slide').parent('selection-item').find('.name').addClass('hide')
           },
           width:"50%",
       },1500);
   }
});