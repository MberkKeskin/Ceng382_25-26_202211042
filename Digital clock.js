function clock(){
    var monthNames = [
        "January", "February", "March", "April", "May", "June", 
        "July", "August", "September", "October", "November", "December"
    ];
    var daYnames = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
    var today=new Date();
    document.getElementById('Date').innerHTML=(daYnames[today.getDay()]+" "+ today.getDate()+' '+ monthNames[today.getMonth()]+' '+ today.getFullYear());
    var h= today.getHours();
    var m=today.getMinutes();
    var s= today.getSeconds();

    h=h<10? '0'+h:h;
    m=m<10? '0'+m:m;
    s=s<10? '0'+s:s;

    document.getElementById('hours').innerHTML=h;
    document.getElementById('min').innerHTML=m;
    document.getElementById('sec').innerHTML=s;
}var inter= setInterval(clock,400);

document.addEventListener('keydown', function(event) {
    if (event.key === 'H' || event.key === 'h') {
        const loginForm = document.querySelector('.login');
        
        if (loginForm.style.display === 'none') {
            loginForm.style.display = 'block'; 
        } else {
            loginForm.style.display = 'none'; 
        }
    }
});

