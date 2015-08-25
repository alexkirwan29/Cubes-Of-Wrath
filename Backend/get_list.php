<?php
require_once("config.php");

// setup some varibles
$page = 0;
$pageSize = 15;
$orderBy = "date";
$allowedOrders = array("date", "title", "author", "likes", "dislikes", "plays");

if(isset($_GET['page']))
    $page = $_GET['page'];

if(isset($_GET['size']))
    $pageSize = $_GET['size'];

if(isset($_GET['order']))
{
    $key = array_search($_GET['order'],$allowedOrders);
    $orderBy=$allowedOrders[$key];
}
$pageStart = $page * $pageSize;

// connect to the database
$mysqli = new mysqli(DB_SERVER, DB_USER, DB_PASSWORD, DB_NAME);

if($mysqli->connect_error)
    die("$mysqli->connect_errno: $mysql->connect_error");

//-------- Get the total level count --------------
if(!$countResult = $mysqli->query("SELECT count(*) as total from user_levels"))
    die("Server Error 3");
$count = mysqli_fetch_array($countResult)[0];
$countResult->close();
//-------------------------------------------------

//------- Get a page from the database ------------
if (!$stmt = $mysqli->prepare("SELECT id, date, title, author, likes, dislikes, plays FROM user_levels ORDER BY $orderBy DESC LIMIT ?,?"))
    die("Server Error 4");

$stmt->bind_param('ii', $pageStart, $pageSize);
$stmt->execute();

/* bind variables to prepared statement */
$stmt->bind_result($id, $date, $title, $author, $likes, $dislikes, $plays);
//-------------------------------------------------

//----------- Create the json text ----------------
$json = array();

$json['version'] = 1;
$json['page count'] = ceil($count / $pageSize);
$json['total levels'] = $count;
$json['time created'] = "realtime";

while($stmt->fetch())
{
    $json['level list'][] = array('id'=>$id, 'date'=>$date, 'title'=>$title, 'author'=>$author, 'likes'=>$likes, 'dislikes'=>$dislikes, 'plays'=>$plays);
}

print(json_encode($json));
//--------------------------------------------------

//close the connections
$stmt->close();
$mysqli->close();
?>