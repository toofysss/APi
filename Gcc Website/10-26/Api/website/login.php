<?php
include '../conn.php';

if (isset($_POST['user']) && isset($_POST['password'])) {
  $user = filterRequest('user');
  $password = filterRequest('password');
  $sqlUser = "SELECT T_Conference_User.email as email ,T_Conference_User.id as id,T_Conference_User.FullName as FullName,T_Conference_User.pass as pass FROM T_Conference_User WHERE T_Conference_User.email = '$user'";
  $stmt = sqlsrv_query($conn, $sqlUser);
  $result = array();

  if (sqlsrv_has_rows($stmt)) {
    while ($row = sqlsrv_fetch_array($stmt)) {
      if (password_verify($password, $row['pass'])) {
        $result[] = $row;
        echo json_encode(array("status" => "success", "data" => $result));
      }

    }
  } else {
    echo json_encode(array("status" => "faild", "data" => $result));

  }
}