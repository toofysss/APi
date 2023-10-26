<?php
include '../conn.php';

if (isset($_POST['domin']) && isset($_POST['count'])) {
    if ($conn) {
        $domin =  filterRequest('domin');
        $count = filterRequest('count');
        if ($count == 0) {
            $query = "SELECT  products.id as id ,products.name as namee ,products.imgfile as imgfile ,products.discr as discr FROM products
                    INNER JOIN dominid ON products.domin = dominid.id
                    WHERE dominid.domin = ? ORDER BY products.id DESC ";
            $queryparams = array($domin);
            $stmt = sqlsrv_query($conn, $query, $queryparams);
            $result = array();
            while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
                $result[] = $row;
            }
            echo json_encode(array("status" => "success", "data" => $result));
        } else {
            $query = "SELECT TOP  $count   products.id AS id, products.name AS namee,products.imgfile AS imgfile, products.discr AS discr
                        FROM products
                        INNER JOIN dominid ON products.domin = dominid.id WHERE dominid.domin = ?
                        ORDER BY products.id DESC";
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
} else if (isset($_POST['domin']) && isset($_POST['id'])) {
    $productID = filterRequest('id');
    $domin =  filterRequest('domin');
    $query = "SELECT products.id as id, products.file_ as filee, products.imgfile as img, products.name as name, products.servicesID as seid,
                    products.origencountryid as orid, products.discr as disc, brand.name as brand,
                    origencountry.name as oricountry,
                    services.name as sename, depart.name as dname
                FROM products
                INNER JOIN services ON products.servicesID = services.id
                INNER JOIN depart ON products.departID = depart.id
                INNER JOIN brand ON products.brandid = brand.id
                INNER JOIN origencountry ON products.origencountryid = origencountry.id
                INNER JOIN dominid ON products.domin = dominid.id
                WHERE dominid.domin = ? AND products.id=? ";
    $queryparams = array($domin,  $productID);
    $stmt = sqlsrv_query($conn, $query, $queryparams);
    $result = array();
    while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
        $result[] = $row;
    }
    echo json_encode(array("status" => "success", "data" => $result));
}
