@script RequireComponent(UnityEngine.UI.Text)
var url = "203.143.87.76/motd.html";
function Start ()
{
    var guiwww : WWW = new WWW(url);
    yield guiwww;
 
    GetComponent(UnityEngine.UI.Text).text = guiwww.text;
}