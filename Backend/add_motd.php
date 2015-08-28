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
    
    echo $motd;
    echo $ttl;
    
    // Open up a file and get it's contents then close it.
    $file = file_get_contents("motd.json");
    $data = json_decode($file);
    unset($file);
    
    // Add the new motd to the list
    $data[] = array('motd'=>$motd,'ttl'=>$ttl);
    
    // Stuff the data into a file that overrided the starting file
    file_put_contents("motd.json", json_encode($data));
    unset($data);
    
    
    // Retun some message.
    echo "Success? I think it worked";
}
else
{
    // Tell us that it failed!
    echo "Failed! No data was sent!";
}
?>