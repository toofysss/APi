
<?php
include '../conn.php';
if (isset($_POST['domin'])) {
    if ($conn) {
        $domin =  filterRequest('domin');
        if ($domin == 0) {
            $server = "gcc.iq";
            // $server = "localhost";
            $query = "SELECT typeblog.id as id, typeblog.type as type FROM typeblog INNER JOIN dominid ON typeblog.domin = dominid.id
                        WHERE dominid.domin = ? ORDER BY typeblog.id DESC";
            $queryparams = array($server);
            $stmt = sqlsrv_query($conn, $query, $queryparams);
            $result = array();

            while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
                $result[] = $row;
            }
            echo json_encode(array("status" => "success", "data" => $result));
        } else {
            $query = "SELECT typeblog.id as id, typeblog.type as type FROM typeblog INNER JOIN dominid ON typeblog.domin = dominid.id
                        WHERE dominid.domin = ? ORDER BY typeblog.id DESC";
            $queryparams = array($domin);
            $stmt = sqlsrv_query($conn, $query, $queryparams);
            $result = array();
            while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
                $result[] = $row;
            }
            echo json_encode(array("status" => "success", "data" => $result));
        }
    } else {
        echo "Connection could not be established.<br />";
        die(print_r(sqlsrv_errors(), true));
    }
}
