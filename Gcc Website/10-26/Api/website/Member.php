
<?php
include '../conn.php';

if (isset($_POST['edit'])) {
    $edit = filterRequest('edit');
    if ($edit == "true") {
        if (isset($_POST['domin']) && isset($_POST['id'])) {
            if ($conn) {
                $domin =  filterRequest('domin');
                $MemberID =  filterRequest('domin');

                $query = "SELECT Council_Member.id as id, Council_Member.Photo_Member as Photo_Member, Council_Member.Position_Member as Position_Member ,Council_Member.Name_Member as Name_Member
                    FROM Council_Member
                    INNER JOIN dominid ON Council_Member.domin = dominid.id
                    WHERE dominid.domin = ? AND Council_Member.id=? ORDER BY Council_Member.id ASC";
                $queryparams = array($domin, $MemberID);
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
    } else {
        if (isset($_POST['domin'])) {
            if ($conn) {
                $domin =  filterRequest('domin');
                $query = "SELECT Council_Member.id as id, Council_Member.Photo_Member as Photo_Member, Council_Member.Position_Member as Position_Member ,Council_Member.Name_Member as Name_Member
                    FROM Council_Member
                    INNER JOIN dominid ON Council_Member.domin = dominid.id
                    WHERE dominid.domin = ? ORDER BY Council_Member.id ASC";
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
    }
}
