

<?php
include '../conn.php';
if (isset($_POST['domin']) && isset($_POST['count'])) {
    if ($conn) {
        $domin =  filterRequest('domin');
        $count = filterRequest('count');
        $sqls = "";
        if ($count == 0) {
            if (isset($_POST['typeID'])) {
                $typeID = $_POST["typeID"];

                if (!empty($typeID)) {
                    $sqls .= " AND blog.typeblogID = " . $typeID;
                }
            }
            $query = "SELECT *, typeblog.type as typeblog FROM blog INNER JOIN typeblog ON blog.typeblogID = typeblog.id INNER JOIN dominid ON blog.domin = dominid.id
                            WHERE dominid.domin = ? $sqls ORDER BY blog.id DESC ";
            $queryparams = array($domin);
            $stmt = sqlsrv_query($conn,$query,$queryparams);
            $result = array();

            while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
                $result[] = $row;
            }
            echo json_encode(array("status" => "success", "data" => $result));
        } else {
            $query = "SELECT *, typeblog.type as typeblog FROM blog
                    INNER JOIN typeblog ON blog.typeblogID = typeblog.id
                    INNER JOIN dominid ON blog.domin = dominid.id
                    WHERE dominid.domin = ? ORDER BY blog.id DESC OFFSET 0 ROWS FETCH NEXT  $count ROWS ONLY";
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
    $blogId = filterRequest('id');
    $domin =  filterRequest('domin');
    $query = "SELECT blog.id as id, blog.title as title, blog.file_ as files,blog.Image4 as Image4,blog.Image1 as Image1,blog.Image2 as Image2,blog.Image3 as Image3, blog.discr as discr, blog.link as link, blog.date , typeblog.type as tyname 
                FROM blog
                INNER JOIN typeblog ON blog.typeblogID = typeblog.id
                INNER JOIN dominid ON blog.domin = dominid.id
                WHERE dominid.domin = ? AND blog.id = ?";
    $queryparams = array($domin, $blogId);
    $stmt = sqlsrv_query($conn, $query, $queryparams);
    $result = array();

    while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
        $result[] = $row;
    }
    echo json_encode(array("status" => "success", "data" => $result));
} else {
    echo "sa";
}
