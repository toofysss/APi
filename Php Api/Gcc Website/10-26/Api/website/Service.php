<?php
include '../conn.php';

if (isset($_POST['domin']) && isset($_POST['count'])) {
    if ($conn) {
        $domin =  filterRequest('domin');
        $count = filterRequest('count');
        if ($count == 0) {
            $query = "SELECT * FROM services 
                    INNER JOIN dominid ON services.domin = dominid.id WHERE dominid.domin = ?
                    ORDER BY services.id DESC ";
            $queryparams = array($domin);
            $stmt = sqlsrv_query($conn, $query, $queryparams);
            $result = array();
            while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
                $result[] = $row;
            }
            echo json_encode(array("status" => "success", "data" => $result));
        } else {
            $query = "SELECT * FROM services 
                    INNER JOIN dominid ON services.domin = dominid.id WHERE dominid.domin = ?
                    ORDER BY services.id DESC OFFSET 0 ROWS FETCH NEXT  $count  ROWS ONLY";
            $queryparams = array($domin);
            $stmt = sqlsrv_query($conn, $query, $queryparams);
            $result = array();
            while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
                $result[] = $row;
            }
            echo json_encode(array("status" => "success", "data" => $result));
        }
    } else {
        echo "Connection could not be established.<br/>";
        die(print_r(sqlsrv_errors(), true));
    }
} else {
    if ($conn) {
        $domin =  filterRequest('domin');

        $query = "SELECT * FROM services INNER JOIN dominid ON services.domin = dominid.id WHERE dominid.domin = ? ORDER BY services.id DESC";
        $queryparams = array($domin);
        $stmt = sqlsrv_query($conn, $query, $queryparams);
        $result = array();
        while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
            $result[] = $row;
        }
        echo json_encode(array("status" => "success", "data" => $result));
    } else {
        echo "Connection could not be established.<br/>";
        die(print_r(sqlsrv_errors(), true));
    }
}
