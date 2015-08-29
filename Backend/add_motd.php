<?php
//Set up the default varibles
$motd = "none";
$ttl = 20;

// Only add stuff to the json file if we have the data.
if(isset($_GET['motdtext']) && isset($_GET['motdttl']))
{
    // Get the data from the url and put it in to some varibles
    $motd = $_GET['motdtext'];
    $ttl = $_GET['motdttl'];

    // Open up a file and get it's contents then close it.
    $file = file_get_contents("motd.json");
    $data = json_decode($file);
    unset($file);
    
    // Add the new motd to the list
    $data[] = array('motd'=>$motd,'ttl'=>$ttl);
    
    // Stuff the data into a file that overrided the starting file
    file_put_contents("motd.json", json_encode($data, JSON_PRETTY_PRINT));
    unset($data);
    
    
    // Retun some messages.
    echo "<h2>Success? I think it worked</h2>";
    echo "<p>Your MOTD will last for $ttl seconds (that can be changed in game) and says<br>$motd</p>";
    echo "<p><a href=\"motd.json\">Click to view the motd.json file</a></p>";
}
else
{
    // Tell us that it failed!
    echo "<h2>Failed! No data was sent!</h2>";
    echo "<p>you might have typed a string that caused an error... Check the url after the ?motdtext=</p>";
}
?>