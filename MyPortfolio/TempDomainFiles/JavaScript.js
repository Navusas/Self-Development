var i = 0;
var txt = 'This website is currently under development...';
var speed = 40;
var firstTime = true;


var j = 0;
var secondSpeed = 40;
var secondTXT = "In the meantime, visit my LinkedIn:"
var secondTime = true;

function firstLineWritter() {
    if (i < txt.length) {
        document.getElementById("firstLine").innerHTML += txt.charAt(i);
        i++;
        setTimeout(firstLineWritter, speed);
    }
    else {
        if (firstTime) {
            setTimeout(firstLineWritter, 200);
            firstTime = false;
        }
        else {
            secondLineWritter();
        }
    }
}

function secondLineWritter() {
    if (j < secondTXT.length) {
        document.getElementById("secondLine").innerHTML += secondTXT.charAt(j);
        j++;
        setTimeout(secondLineWritter, secondSpeed);
    }
    else {
        if (secondTime) {
            setTimeout(secondLineWritter, 200);
            secondTime = false;
        }
        else {
            document.getElementById("urlID").style.display = "block";
        }
    }
}