<?php
require_once(__DIR__ . '/../Data/connections.php');
require_once(__DIR__ . '/../Data/function.php');



$conn = sqlsrv_connect($servername, $connectionOptions);
