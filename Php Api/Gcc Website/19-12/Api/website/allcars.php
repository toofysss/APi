<?php
include '../conn.php';


if (isset($_POST['domin']) && isset($_POST['date'])) {
    if ($conn) {
        $date = filterRequest('date');
        $sql = "";
        // if ($date == '0') {
        //     $sql .= "AND Cars_T_Details.IsSold = 0 ";
        // } else {
        //     $sql .= "AND Cars_T_Details.IsSold = 1";
        // }

        if (isset($_POST['brand']) || isset($_POST['ManufactureDate']) || isset($_POST['DscrpType']) || isset($_POST['DscrpCountry']) || isset($_POST['DscrpSwitchType']) || isset($_POST['DscrpGeartype']) || isset($_POST['EnginSize']) || isset($_POST['StateCar']) || isset($_POST['DscrpColor']) || isset($_POST['DscrpGovernorate']) || isset($_POST['DscrpFueltype'])) {
            $Brand = $_POST["brand"];
            $ManufactureDate = $_POST["ManufactureDate"];
            $Type = $_POST["DscrpType"];
            $Country = $_POST["DscrpCountry"];
            $SwitchType = $_POST["DscrpSwitchType"];
            $Geartype = $_POST["DscrpGeartype"];
            $EnginSize = $_POST["EnginSize"];
            $StateCar = $_POST["StateCar"];
            $DscrpColor = $_POST["DscrpColor"];
            $DscrpGovernorate = $_POST["DscrpGovernorate"];
            $DscrpFueltype = $_POST["DscrpFueltype"];
            $sql = "";

            if (!empty($Brand)) {
                $sql .= " AND Cars_T_Details.BrandID = " . $Brand;
            }
            if (!empty($ManufactureDate)) {
                $sql .= " AND Cars_T_Details.ManufactureDate = " . $ManufactureDate;
            }
            if (!empty($Type)) {
                $sql .= " AND Cars_T_Details.CarClassID = " . $Type;
            }
            if (!empty($Country)) {
                $sql .= " AND Cars_T_Details.CarOrigenID = " . $Country;
            }
            if (!empty($SwitchType)) {
                $sql .= " AND Cars_T_Details.SwitchTypeID = " . $SwitchType;
            }
            if (!empty($Geartype)) {
                $sql .= " AND Cars_T_Details.Geartype = " . $Geartype;
            }
            if (!empty($EnginSize)) {
                $sql .= " AND Cars_T_Details.EnginSize = " . $EnginSize;
            }
            if (!empty($StateCar)) {
                $sql .= " AND Cars_T_Details.CarStateID = " . $StateCar;
            }
            if (!empty($DscrpColor)) {
                $sql .= " AND Cars_T_Details.ColorID = " . $DscrpColor;
            }
            if (!empty($DscrpGovernorate)) {
                $sql .= " AND Cars_T_Details.GovernorateID = " . $DscrpGovernorate;
            }
            if (!empty($DscrpFueltype)) {
                $sql .= " AND Cars_T_Details.FuelType = " . $DscrpFueltype;
            }
        }
        $query = "SELECT (SELECT COUNT(*) AS totalRows FROM Cars_T_Details WHERE Cars_T_Details.IsSold IS NOT NULL AND Cars_T_Details.IsSold=$date  $sql) As  table_length,
                        Cars_T_Details.CarTitle as CarTitle,Cars_T_Details.EnginSize as EnginSize,Cars_T_Details.DamageTaype as DamageTaype,Cars_T_Details.KM_Running as KM_Running, Cars_T_Details.id as id, Cars_T_Details.ManufactureDate as ManufactureDate,Cars_T_Details.ShaseNumber as ShaseNumber, Cars_T_Details.Specifications as Specifications, Cars_T_Details.PriceAmount as PriceAmount, 
                        Car_Vehicle_Type.Dscrp as DscrpType,Cars_P_SwitchType.Dscrp as DscrpSwitchType,Cars_p_Geartype.Dscrp as DscrpGeartype,Cars_P_Countertype.Dscrp as DscrpCountertype, Car_vehicleBrands.Dscrp as brand, Car_vehicles__State.Dscrp as StateCar,
                        Car_Currencies.shortcurrencies as currencies,system.name as name,system.name_eng as name_eng,system.file_ as file_, CarColors.Dscrp as DscrpColor,P_Country.Country as DscrpCountry,gp_Governorate.Dscrp as DscrpGovernorate ,Cars_P_Fueltype.Dscrp as DscrpFueltype
                        FROM Cars_T_Details
                        INNER JOIN Cars_P_SwitchType ON Cars_P_SwitchType.id = Cars_T_Details.SwitchTypeID   
                        INNER JOIN Cars_p_Geartype ON Cars_p_Geartype.id = Cars_T_Details.Geartype   
                        INNER JOIN Cars_P_Countertype ON Cars_P_Countertype.id = Cars_T_Details.CounterType   
                        INNER JOIN Cars_P_Fueltype ON Cars_P_Fueltype.id = Cars_T_Details.FuelType   
                        INNER JOIN gp_Governorate ON gp_Governorate.id = Cars_T_Details.GovernorateID   
                        INNER JOIN P_Country ON P_Country.id = Cars_T_Details.CarOrigenID   
                        INNER JOIN CarColors ON CarColors.id = Cars_T_Details.ColorID   
                        INNER JOIN Car_vehicles__State ON Car_vehicles__State.id = Cars_T_Details.CarStateID          
                        INNER JOIN Car_Vehicle_Type ON Car_Vehicle_Type.id = Cars_T_Details.CarClassID
                        INNER JOIN Car_vehicleBrands ON Car_vehicleBrands.id = Cars_T_Details.BrandID
                        INNER JOIN system ON system.domin = Cars_T_Details.domin
                        INNER JOIN Car_Currencies ON Car_Currencies.id = Cars_T_Details.CurrencyID
                        WHERE Cars_T_Details.IsSold IS NOT NULL AND Cars_T_Details.IsSold=$date $sql
                        ORDER BY Cars_T_Details.id";


        $image = "SELECT * FROM T_CarImage WHERE CarID IN (SELECT Cars_T_Details.id FROM Cars_T_Details)";

        $stmt = sqlsrv_query($conn, $query);
        $stmtimage = sqlsrv_query($conn, $image);
        $result = array();
        $carImages = array();
        while ($stmtimagerow = sqlsrv_fetch_array($stmtimage, SQLSRV_FETCH_ASSOC)) {
            $carID = $stmtimagerow["CarID"];
            $imagePath = $stmtimagerow["Dscrp"];
            if (!isset($carImages[$carID])) {
                $carImages[$carID] = array();
            }
            $carImages[$carID][] = $imagePath;
        }
        while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
            $carID = $row["id"];
            $carData = array(
                "car_id" => $carID,
                "name_ar" => $row["name"],
                "table_length" => $row["table_length"],
                "name_eng" => $row["name_eng"],
                "imagefile" => $row["file_"],
                "car" => array(
                    "carname" => $row["CarTitle"],
                    "ManufactureDate" => $row["ManufactureDate"],
                    "CarType" => $row["DscrpType"],
                    "brand" => $row["brand"],
                    "price" => $row["PriceAmount"],
                    "currency" => $row["currencies"],
                    "imagepath" => isset($carImages[$carID]) ? $carImages[$carID] : []
                )
            );
            $result[] = $carData;
        }

        $response = array("cars" => $result);
        echo json_encode($response, JSON_UNESCAPED_UNICODE);

    }

}