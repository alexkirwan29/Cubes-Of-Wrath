<?php
require_once("config.php");

// setup some varibles
$levelId= 0;

if(isset($_GET['id']))
    $levelId= $_GET['id'];

// connect to the database
$mysqli = new mysqli(DB_SERVER, DB_USER, DB_PASSWORD, DB_NAME);

if($mysqli->connect_error)
    die("$mysqli->connect_errno: $mysql->connect_error");

//------- Get a page from the database ------------
if (!$stmt = $mysqli->prepare("SELECT id, date, title, author, likes, dislikes, plays, description, level_path FROM user_levels WHERE id = ?"))
    die("Server Error 4");

$stmt->bind_param('i', $levelId);
$stmt->execute();

/* bind variables to prepared statement */
$stmt->bind_result($id, $date, $title, $author, $likes, $dislikes, $plays, $description, $data);
//-------------------------------------------------

//----------- Create the json text ----------------
$json = array();
while($stmt->fetch())
{
    $json['id'] = $id;
    $json['date'] = $date;
    $json['title'] = $title;
    $json['author'] = $author;
    $json['likes'] = $likes;
    $json['dislikes'] = $dislikes;
    $json['plays'] = $plays;
    $json['description'] = $description;
    $json['data url'] = $data;
}

print(json_encode($json));
//--------------------------------------------------

//close the connections
$stmt->close();
$mysqli->close();
?>