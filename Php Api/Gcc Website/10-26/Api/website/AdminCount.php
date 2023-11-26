
<?php
include '../conn.php';
if (isset($_POST['domin'])) {
    if ($conn) {
        $domin =  filterRequest('domin');
        $query = "SELECT
                    (SELECT COUNT(*) FROM services) AS services_count,
                    (SELECT COUNT(*) FROM depart) AS departments_count,
                    (SELECT COUNT(*) FROM products) AS products_count,
                    (SELECT COUNT(*) FROM blog) AS blog_count
                    FROM dominid
                    WHERE dominid.domin =?";
        $queryparams = array($domin);
        $stmt = sqlsrv_query($conn, $query, $queryparams);
        $result = array();
        while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
            $result[] = $row;
        }
        echo json_encode(array("status" => "success", "data" => $result));
    } else {
        echo "Connection could not be established.<br />";
        die(print_r(sqlsrv_errors(), true));
    }
}
