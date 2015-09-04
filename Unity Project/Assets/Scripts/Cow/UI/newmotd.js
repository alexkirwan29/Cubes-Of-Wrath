var url = "203.143.87.76/motd.html"; var guiText1 : UnityEngine.UI.Text;

  function Start () {
      var guiwww : WWW = new WWW(url);
      yield guiwww;
 
      guiText1.text = guiwww.text;
}



