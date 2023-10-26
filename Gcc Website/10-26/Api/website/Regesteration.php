<?php
include '../conn.php';

use PHPMailer\PHPMailer\PHPMailer;
use PHPMailer\PHPMailer\Exception;

require '../../PHPMailer/src/Exception.php';
require '../../PHPMailer/src/PHPMailer.php';
require '../../PHPMailer/src/SMTP.php';


if (isset($_POST['companyar']) && isset($_POST['companyen']) && isset($_POST['activity']) && isset($_POST['domin']) && isset($_POST['email']) && isset($_POST['phone']) && isset($_POST['types']) && isset($_POST['worktitie'])) {
    $DscrpA = filterRequest('companyar');
    $DscrpE = filterRequest('companyen');
    $Activities = filterRequest('activity');
    $E_Mail = filterRequest('email');
    $Phone = filterRequest('phone');
    $WorkAdress = filterRequest('worktitie');
    $IraqGuideId = filterRequest('types');
    $domin = filterRequest('domin');
    $LogoImg = imageUpload('logo', "../../$locationImg/requirements/");
    $IdentifireImgFront = imageUpload('frontimage', "../../$locationImg/requirements/");
    $IdentifireImgBack = imageUpload('backimage', "../../$locationImg/requirements/");
    $LiveCardFront = imageUpload('nationalcardfrontimage', "../../$locationImg/requirements/");
    $BackGroundImg = imageUpload('background', "../../$locationImg/requirements/");
    $AddressImgFront = imageUpload('residencecardfrontimage', "../../$locationImg/requirements/");
    $AddressImgBack = imageUpload('residencecardbackimage', "../../$locationImg/requirements/");
    $LiveCardBack = imageUpload('nationalcardbackimage', "../../$locationImg/requirements/");

    $query = "INSERT INTO T_CompaniesRegstrations_Orders (DscrpA, DscrpE, Activities, E_Mail, WorkAdress, Phone, domin, LogoImg, IdentifireImgFront, IdentifireImgBack, LiveCardFront, LiveCardBack, BackGroundImg, AddressImgFront, AddressImgBack,IraqGuideId) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?)";
    $params = array($DscrpA, $DscrpE, $Activities, $E_Mail, $WorkAdress, $Phone, $domin, $LogoImg, $IdentifireImgFront, $IdentifireImgBack, $LiveCardFront, $LiveCardBack, $BackGroundImg, $AddressImgFront, $AddressImgBack, $IraqGuideId);
    $stmt = sqlsrv_query($conn, $query, $params);
    try {
        $mail = new PHPMailer(true);

        require "../../Data/email.php";

        $mail->addAddress($E_Mail, 'Admin Name');

        // Email content
        $mail->isHTML(true);
        $mail->CharSet = 'UTF-8';
        $mail->Subject = ' طلب التسجيل  ';
        $mail->isHTML(true);

        // HTML body with embedded CSS
        $mail->Body = '
                    <!DOCTYPE html>

                    <html
                      lang="en"
                      xmlns:o="urn:schemas-microsoft-com:office:office"
                      xmlns:v="urn:schemas-microsoft-com:vml"
                    >
                      <head>
                        <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
                        <meta content="width=device-width, initial-scale=1.0" name="viewport" />
                        <!--[if mso
                          ]><xml
                            ><o:OfficeDocumentSettings
                              ><o:PixelsPerInch>96</o:PixelsPerInch
                              ><o:AllowPNG /></o:OfficeDocumentSettings></xml
                        ><![endif]-->
                        <!--[if !mso]><!-->
                        <link
                          href="https://fonts.googleapis.com/css?family=Noto+Serif"
                          rel="stylesheet"
                          type="text/css"
                        />
                        <link
                          href="https://fonts.googleapis.com/css2?family=Inter&family=Work+Sans:wght@700&display=swap"
                          rel="stylesheet"
                          type="text/css"
                        />
                        <!--<![endif]-->
                        <style 
                          @import url("https://fonts.googleapis.com/css2?family=Cairo:wght@300;400;500;700;900&display=swap");
                          @import url("https://fonts.googleapis.com/css2?family=El+Messiri:wght@500&family=Lato:wght@400;900&display=swap");
                          * {
                            box-sizing: border-box;
                          }
                    
                          body {
                            margin: 0;
                            padding: 0;
                          }
                    
                          a[x-apple-data-detectors] {
                            color: inherit !important;
                            text-decoration: inherit !important;
                          }
                    
                          #MessageViewBody a {
                            color: inherit;
                            text-decoration: none;
                          }
                    
                          p {
                            line-height: inherit;
                          }
                    
                          .desktop_hide,
                          .desktop_hide table {
                            mso-hide: all;
                            display: none;
                            max-height: 0px;
                            overflow: hidden;
                          }
                    
                          .image_block img + div {
                            display: none;
                          }
                    
                          @media (max-width: 720px) {
                            .desktop_hide table.icons-inner {
                              display: inline-block !important;
                       
                    
                            .icons-inner {
                              text-align: center;
                            }
                    
                            .icons-inner td {
                              margin: 0 auto;
                            }
                    
                            .mobile_hide {
                              display: none;
                            }
                    
                            .row-content {
                              width: 100% !important;
                            }
                    
                            .stack .column {
                              width: 100%;
                              display: block;
                            }
                    
                            .mobile_hide {
                              min-height: 0;
                              max-height: 0;
                              max-width: 0;
                              overflow: hidden;
                              font-size: 0px;
                            }
              
                            .desktop_hide,
                            .desktop_hide table {
                              display: table !important;
                              max-height: none !important;
                            }
                    
                            .row-13 .column-1 .block-1.heading_block h1,
                            .row-13 .column-2 .block-1.paragraph_block td.pad > div,
                            .row-14 .column-2 .block-1.paragraph_block td.pad > div,
                            .row-2 .column-2 .block-1.paragraph_block td.pad > div,
                            .row-3 .column-1 .block-4.heading_block h1,
                            .row-6 .column-2 .block-1.heading_block h1,
                            .row-6 .column-2 .block-2.heading_block h1,
                            .row-6 .column-2 .block-3.paragraph_block td.pad > div,
                            .row-7 .column-2 .block-1.heading_block h1,
                            .row-7 .column-2 .block-2.heading_block h1,
                            .row-7 .column-2 .block-3.paragraph_block td.pad > div {
                              text-align: center !important;
                            }
                    
                            .row-13 .column-2 .block-1.paragraph_block td.pad {
                              padding: 0 !important;
                            }
                    
                            .row-14 .column-1,
                            .row-2 .column-1,
                            .row-4 .column-1,
                            .row-9 .column-1 {
                              padding: 20px 10px !important;
                            }
                    
                            .row-2 .column-2 {
                              padding: 5px 25px 20px !important;
                            }
                    
                            .row-6 .column-1,
                            .row-7 .column-1 {
                              padding: 15px 25px 0 !important;
                            }
                    
                            .row-6 .column-2,
                            .row-7 .column-2 {
                              padding: 15px 20px 25px !important;
                            }
                    
                            .row-10 .column-1 {
                              padding: 40px 20px !important;
                            }
                    
                            .row-12 .column-1 {
                              padding: 0 20px 40px !important;
                            }
                    
                            .row-13 .column-1 {
                              padding: 40px 25px 25px !important;
                            }
                    
                            .row-13 .column-2 {
                              padding: 5px 25px 40px !important;
                            }
                    
                            .row-14 .column-2 {
                              padding: 5px 30px 20px 25px !important;
                            }
                          }
                          .send_form_btn{
                            text-align: center;
                            padding:5px;
                          }
                         
                        </style>
                      </head>
                      <body
                        style="
                          background-color: #f7f7f7;
                          margin: 0;
                          padding: 0;
                          -webkit-text-size-adjust: none;
                          text-size-adjust: none;
                          
                        "
                      >
                        <table
                          border="0"
                          cellpadding="0"
                          cellspacing="0"
                          class="nl-container"
                          role="presentation"
                          style="
                            mso-table-lspace: 0pt;
                            mso-table-rspace: 0pt;
                            background-color: #f7f7f7;
                          "
                          width="100%"
                        >
                          <tbody>
                            <tr>
                              <td>
                                <table
                                  align="center"
                                  border="0"
                                  cellpadding="0"
                                  cellspacing="0"
                                  class="row row-1"
                                  role="presentation"
                                  style="mso-table-lspace: 0pt; mso-table-rspace: 0pt"
                                  width="100%"
                                >
                                  <tbody>
                                    <tr>
                                      <td>
                                        <table
                                          align="center"
                                          border="0"
                                          cellpadding="0"
                                          cellspacing="0"
                                          class="row-content stack"
                                          role="presentation"
                                          style="
                                            mso-table-lspace: 0pt;
                                            mso-table-rspace: 0pt;
                                            border-radius: 0;
                                            color: #000;
                                            width: 700px;
                                            margin: 0 auto;
                                          "
                                          width="700"
                                        >
                                          <tbody>
                                            <tr>
                                              <td
                                                class="column column-1"
                                                style="
                                                  mso-table-lspace: 0pt;
                                                  mso-table-rspace: 0pt;
                                                  font-weight: 400;
                                                  text-align: left;
                                                  padding-bottom: 5px;
                                                  padding-top: 5px;
                                                  vertical-align: top;
                                                  border-top: 0px;
                                                  border-right: 0px;
                                                  border-bottom: 0px;
                                                  border-left: 0px;
                                                "
                                                width="100%"
                                              >
                                                <div
                                                  class="spacer_block block-1"
                                                  style="
                                                    height: 15px;
                                                    line-height: 15px;
                                                    font-size: 1px;
                                                  "
                                                >
                                                </div>
                                              </td>
                                            </tr>
                                          </tbody>
                                        </table>
                                      </td>
                                    </tr>
                                  </tbody>
                                </table>
                                <table
                                  align="center"
                                  border="0"
                                  cellpadding="0"
                                  cellspacing="0"
                                  class="row row-2"
                                  role="presentation"
                                  style="mso-table-lspace: 0pt; mso-table-rspace: 0pt"
                                  width="100%"
                                >
                                  <tbody>
                                    <tr>
                                      <td>
                                        <table
                                          align="center"
                                          border="0"
                                          cellpadding="0"
                                          cellspacing="0"
                                          class="row-content stack"
                                          role="presentation"
                                          style="
                                            mso-table-lspace: 0pt;
                                            mso-table-rspace: 0pt;
                                            background-image: url(\'http://gcc.iq/assets/images/bg.png\');
                                            background-repeat: no-repeat;
                                            background-size: cover;
                                            background-color: #002e5c;
                                            border-radius: 0;
                                            color: #000;
                                            width: 700px;
                                            margin: 0 auto;
                                          "
                                          width="700"
                                        >
                                          <tbody>
                                            <tr>
                                              <td
                                                class="column column-1"
                                                style="
                                                  mso-table-lspace: 0pt;
                                                  mso-table-rspace: 0pt;
                                                  font-weight: 400;
                                                  text-align: left;
                                                  padding-bottom: 20px;
                                                  padding-left: 30px;
                                                  padding-right: 10px;
                                                  padding-top: 20px;
                                                  vertical-align: middle;
                                                  border-top: 0px;
                                                  border-right: 0px;
                                                  border-bottom: 0px;
                                                  border-left: 0px;
                                                "
                                                width="33.333333333333336%"
                                              >
                                                <table
                                                  border="0"
                                                  cellpadding="0"
                                                  cellspacing="0"
                                                  class="image_block block-1"
                                                  role="presentation"
                                                  style="
                                                    mso-table-lspace: 0pt;
                                                    mso-table-rspace: 0pt;
                                                  "
                                                  width="100%"
                                                >
                                                  <tr>
                                                    <td class="pad" style="width: 100%">
                                                      <div
                                                        align="center"
                                                        class="alignment"
                                                        style="line-height: 10px"
                                                      >
                                                        <a
                                                          href="https://www.example.com"
                                                          style="outline: none"
                                                          tabindex="-1"
                                                          target="_blank"
                                                          ><img
                                                            alt="your-logo"
                                                            src="http://gcc.iq/assets/images/logo.jpeg"
                                                            style="
                                                              display: block;
                                                              border: 0;
                                                              border-radius: 50%;
                                                            "
                                                            title="your-logo"
                                                            width="100"
                                                            height="100"
                                                        /></a>
                                                      </div>
                                                    </td>
                                                  </tr>
                                                </table>
                                              </td>
                                              <td
                                                class="column column-2"
                                                style="
                                                  mso-table-lspace: 0pt;
                                                  mso-table-rspace: 0pt;
                                                  font-weight: 400;
                                                  text-align: left;
                                                  padding-bottom: 5px;
                                                  padding-left: 25px;
                                                  padding-right: 30px;
                                                  padding-top: 5px;
                                                  vertical-align: middle;
                                                  border-top: 0px;
                                                  border-right: 0px;
                                                  border-bottom: 0px;
                                                  border-left: 0px;
                                                "
                                                width="66.66666666666667%"
                                              >
                                                <table
                                                  border="0"
                                                  cellpadding="0"
                                                  cellspacing="0"
                                                  class="paragraph_block block-1"
                                                  role="presentation"
                                                  style="
                                                    mso-table-lspace: 0pt;
                                                    mso-table-rspace: 0pt;
                                                    word-break: break-word;
                                                  "
                                                  width="100%"
                                                >
                                                  <tr>
                                                    <td class="pad">
                                                      <div
                                                        style="
                                                          color: #ffffff;
                                                          direction: ltr;
                                                          font-family: Inter, sans-serif;
                                                          font-size: 16px;
                                                          font-weight: 400;
                                                          letter-spacing: 0px;
                                                          line-height: 120%;
                                                          text-align: right;
                                                          mso-line-height-alt: 19.2px;
                                                        "
                                                      >
                                                        <p
                                                          style="
                                                            margin: 0;
                                                            font-family: \'El Messiri\', sans-serif;
                                                            font-size: 28px;
                                                          "
                                                        >
                                                          منصة العراق الاقتصادية
                                                        </p>
                                                      </div>
                                                    </td>
                                                  </tr>
                                                </table>
                                              </td>
                                            </tr>
                                          </tbody>
                                        </table>
                                      </td>
                                    </tr>
                                  </tbody>
                                </table>
                                <table
                                  align="center"
                                  border="0"
                                  cellpadding="0"
                                  cellspacing="0"
                                  class="row row-3"
                                  role="presentation"
                                  style="mso-table-lspace: 0pt; mso-table-rspace: 0pt"
                                  width="100%"
                                >
                                  <tbody>
                                    <tr>
                                      <td>
                                        <table
                                          align="center"
                                          border="0"
                                          cellpadding="0"
                                          cellspacing="0"
                                          class="row-content stack"
                                          role="presentation"
                                          style="
                                            mso-table-lspace: 0pt;
                                            mso-table-rspace: 0pt;
                                            background-color: #efeef4;
                                            border-bottom: 0 solid #efeef4;
                                            border-left: 0 solid #efeef4;
                                            border-right: 0px solid #efeef4;
                                            border-top: 0 solid #efeef4;
                                            color: #000;
                                            width: 700px;
                                            margin: 0 auto;
                                          "
                                          width="700"
                                        >
                                          <tbody>
                                            <tr>
                                              <td
                                                class="column column-1"
                                                style="
                                                  mso-table-lspace: 0pt;
                                                  mso-table-rspace: 0pt;
                                                  font-weight: 400;
                                                  text-align: left;
                                                  padding-bottom: 25px;
                                                  padding-left: 25px;
                                                  padding-right: 25px;
                                                  padding-top: 35px;
                                                  vertical-align: top;
                                                  border-top: 0px;
                                                  border-right: 0px;
                                                  border-bottom: 0px;
                                                  border-left: 0px;
                                                "
                                                width="100%"
                                              >
                                              <table
                                                  border="0"
                                                  cellpadding="0"
                                                  cellspacing="0"
                                                  class="icons_block block-1"
                                                  role="presentation"
                                                  style="
                                                    mso-table-lspace: 0pt;
                                                    mso-table-rspace: 0pt;
                                                  "
                                                  width="100%"
                                                >
                                                  <tr>
                                                    <td
                                                      class="pad"
                                                      style="
                                                        vertical-align: middle;
                                                        color: #002e5c;
                                                        font-family: \'Noto Serif\', Georgia, serif;
                                                        font-size: 24px;
                                                        letter-spacing: 0px;
                                                        padding-bottom: 10px;
                                                        padding-top: 10px;
                                                        text-align: center;
                                                      "
                                                    >
                                                      <table
                                                        align="center"
                                                        cellpadding="0"
                                                        cellspacing="0"
                                                        class="alignment"
                                                        role="presentation"
                                                        style="
                                                          mso-table-lspace: 0pt;
                                                          mso-table-rspace: 0pt;
                                                        "
                                                      >
                                                        <tr>
                                                          <td
                                                            style="
                                                              vertical-align: middle;
                                                              text-align: center;
                                                              padding-top: 0px;
                                                              padding-bottom: 0px;
                                                              padding-left: 20px;
                                                              padding-right: 20px;
                                                            "
                                                          >
                                                          <script src="https://unpkg.com/@lottiefiles/lottie-player@latest/dist/lottie-player.js"></script>
                                                          <lottie-player src="animation_ll1wxwbj.json" background="Transparent" speed="1" style="width: 300px; height: 300px" direction="1" mode="normal" loop autoplay></lottie-player>
                                                          </td>
                                                        </tr>
                                                      </table>
                                                    </td>
                                                  </tr>
                                                </table>
                                              <table
                                                  border="0"
                                                  cellpadding="0"
                                                  cellspacing="0"
                                                  class="heading_block block-1"
                                                  role="presentation"
                                                  style="
                                                    mso-table-lspace: 0pt;
                                                    mso-table-rspace: 0pt;
                                                  "
                                                  width="100%"
                                                  
                                                >
                                                  <tr>
                                                    <td
                                                      class="pad"
                                                      style="text-align: center; width: 100%"
                                                    >
                                                      <h2
                                                        style="
                                                          margin: 0;
                                                          color: #201f42;
                                                          direction: ltr;
                                                          font-family: \'Cairo\', sans-serif;
                                                          font-size: 24px;
                                                          font-weight: 700;
                                                          letter-spacing: normal;
                                                          line-height: 120%;
                                                          text-align: center;
                                                          margin-top: 0;
                                                          margin-bottom: 0;
                                                        "
                                                      >
                                                    مرحباً    ' . $DscrpA . '
                                                      </h2>
                                                    </td>
                                                  </tr>
                                                </table>
                                                <table
                                                  border="0"
                                                  cellpadding="10"
                                                  cellspacing="0"
                                                  class="paragraph_block block-3"
                                                  role="presentation"
                                                  style="
                                                    mso-table-lspace: 0pt;
                                                    mso-table-rspace: 0pt;
                                                    word-break: break-word;
                                                  "
                                                  width="100%"
                                                >
                                                  <tr>
                                                    <td class="pad">
                                                      <div
                                                        style="
                                                          color: #201f42;
                                                          direction: ltr;
                                                          font-family: \'Cairo\', sans-serif;
                                                          font-size: 22px;
                                                          font-weight: 500;
                                                          letter-spacing: 0px;
                                                          line-height: 180%;
                                                          text-align: center;
                                                          mso-line-height-alt: 25.2px;
                                                        "
                                                      >
                                                        <p style="margin: 0">تم استلام طلبك شكراً لأهتمامك بالتسجيل في منصة العراق الاقتصادية</p>
                                                        <p style="margin: 0">سنقوم بمراجعة طلبك والتواصل مع قريباً  </p>
                                                      </div>
                                                    </td>
                                                  </tr>
                                                </table>
                                                <table
                                                  border="0"
                                                  cellpadding="0"
                                                  cellspacing="0"
                                                  class="button_block block-1"
                                                  role="presentation"
                                                  style="
                                                    mso-table-lspace: 0pt;
                                                    mso-table-rspace: 0pt;
                                                  "
                                                  width="100%"
                                                >
                                                  <tr>
                                                    <td
                                                      class="pad"
                                                      style="
                                                        padding-bottom: 15px;
                                                        padding-top: 30px;
                                                        text-align: center;
                                                      "
                                                    >
                                                      <div align="center" class="alignment">
                                                        <!--[if mso]><v:roundrect xmlns:v="urn:schemas-microsoft-com:vml" xmlns:w="urn:schemas-microsoft-com:office:word" href="https://www.example.com" style="height:44px;width:103px;v-text-anchor:middle;" arcsize="0%" strokeweight="0.75pt" strokecolor="#201F42" fillcolor="#201f42"><w:anchorlock/><v:textbox inset="0px,0px,0px,0px"><center style="color:#ffffff; font-family:Georgia, serif; font-size:16px"><!
                                                        [endif]--><!--[if mso]></center></v:textbox></v:roundrect><![endif]-->
                                                      </div>
                                                    </td>
                                                  </tr>
                                                </table>
                                              </td>
                                            </tr>
                                          </tbody>
                                        </table>
                                      </td>
                                    </tr>
                                  </tbody>
                                </table>
                                <table
                                  align="center"
                                  border="0"
                                  cellpadding="0"
                                  cellspacing="0"
                                  class="row row-13"
                                  role="presentation"
                                  style="mso-table-lspace: 0pt; mso-table-rspace: 0pt"
                                  width="100%"
                                >
                                  <tbody>
                                    <tr>
                                      <td>
                                        <table
                                          align="center"
                                          border="0"
                                          cellpadding="0"
                                          cellspacing="0"
                                          class="row-content stack"
                                          role="presentation"
                                          style="
                                            mso-table-lspace: 0pt;
                                            mso-table-rspace: 0pt;
                                            background-image: url(\'http://gcc.iq/assets/images/bg.png\');
                                            background-repeat: no-repeat;
                                            background-size: cover;
                                            background-color: #002e5c;
                                            border-radius: 0;
                                            color: #000;
                                            width: 700px;
                                            margin: 0 auto;
                                          "
                                          width="700"
                                        >
                                          <tbody>
                                            <tr>
                                              <td
                                                class="column column-1"
                                                style="
                                                  mso-table-lspace: 0pt;
                                                  mso-table-rspace: 0pt;
                                                  font-weight: 400;
                                                  text-align: left;
                                                  padding-bottom: 40px;
                                                  padding-left: 25px;
                                                  padding-right: 25px;
                                                  padding-top: 40px;
                                                  vertical-align: middle;
                                                  border-top: 0px;
                                                  border-right: 0px;
                                                  border-bottom: 0px;
                                                  border-left: 0px;
                                                "
                                                width="50%"
                                              >
                                                <table
                                                  border="0"
                                                  cellpadding="0"
                                                  cellspacing="0"
                                                  class="heading_block block-1"
                                                  role="presentation"
                                                  style="
                                                    mso-table-lspace: 0pt;
                                                    mso-table-rspace: 0pt;
                                                  "
                                                  width="100%"
                                                >
                                                  <tr>
                                                    <td
                                                      class="pad"
                                                      style="
                                                        padding-bottom: 5px;
                                                        padding-left: 10px;
                                                        padding-top: 5px;
                                                        text-align: center;
                                                        width: 100%;
                                                      "
                                                    >
                                                      <h1
                                                        style="
                                                          margin: 0;
                                                          color: #ffffff;
                                                          direction: ltr;
                                                          font-family: \'Cairo\', sans-serif;
                                                          font-size: 30px;
                                                          font-weight: 700;
                                                          letter-spacing: normal;
                                                          line-height: 120%;
                                                          text-align: left;
                                                          margin-top: 0;
                                                          margin-bottom: 0;
                                                        "
                                                      >
                                                        <span class="tinyMce-placeholder"
                                                          >للمزيد من المعلومات<br
                                                        /></span>
                                                      </h1>
                                                    </td>
                                                  </tr>
                                                </table>
                                              </td>
                                              <td
                                                class="column column-2"
                                                style="
                                                  mso-table-lspace: 0pt;
                                                  mso-table-rspace: 0pt;
                                                  font-weight: 400;
                                                  text-align: left;
                                                  padding-bottom: 40px;
                                                  padding-left: 25px;
                                                  padding-right: 25px;
                                                  padding-top: 40px;
                                                  vertical-align: middle;
                                                  border-top: 0px;
                                                  border-right: 0px;
                                                  border-bottom: 0px;
                                                  border-left: 0px;
                                                "
                                                width="50%"
                                              >
                                                <table
                                                  border="0"
                                                  cellpadding="0"
                                                  cellspacing="0"
                                                  class="paragraph_block block-1"
                                                  role="presentation"
                                                  style="
                                                    mso-table-lspace: 0pt;
                                                    mso-table-rspace: 0pt;
                                                    word-break: break-word;
                                                  "
                                                  width="100%"
                                                >
                                                  <tr>
                                                    <td class="pad" style="padding-right: 10px">
                                                      <div
                                                        style="
                                                          color: #ffffff;
                                                          direction: ltr;
                                                          font-family: \'Cairo\', sans-serif;
                                                          font-size: 24px;
                                                          font-weight: 400;
                                                          letter-spacing: 0px;
                                                          line-height: 120%;
                                                          text-align: right;
                                                          mso-line-height-alt: 19.2px;
                                                        "
                                                      >
                                                        <p style="margin: 0">
                                                          <a
                                                            href="https://wa.me/9647809966611"
                                                            rel="noopener"
                                                            style="
                                                              text-decoration: underline;
                                                              color: #ffffff;
                                                            "
                                                            target="_blank"
                                                            ><u>تواصل معنا</u> </a
                                                          >
                                                        </p>
                                                      </div>
                                                    </td>
                                                  </tr>
                                                </table>
                                              </td>
                                            </tr>
                                          </tbody>
                                        </table>
                                      </td>
                                    </tr>
                                  </tbody>
                                </table>
                                <table
                                  align="center"
                                  border="0"
                                  cellpadding="0"
                                  cellspacing="0"
                                  class="row row-14"
                                  role="presentation"
                                  style="
                                    mso-table-lspace: 0pt;
                                    mso-table-rspace: 0pt;
                                    background-size: auto;
                                  "
                                  width="100%"
                                >
                                  <tbody>
                                    <tr>
                                      <td>
                                        <table
                                          align="center"
                                          border="0"
                                          cellpadding="0"
                                          cellspacing="0"
                                          class="row-content stack"
                                          role="presentation"
                                          style="
                                            mso-table-lspace: 0pt;
                                            mso-table-rspace: 0pt;
                                            background-size: auto;
                                            background-color: #002e5c;
                                            border-radius: 0;
                                            color: #000;
                                            width: 700px;
                                            margin: 0 auto;
                                          "
                                          width="700"
                                        >
                                          <tbody>
                                            <tr>
                                              <td
                                                class="column column-1"
                                                style="
                                                  mso-table-lspace: 0pt;
                                                  mso-table-rspace: 0pt;
                                                  font-weight: 400;
                                                  text-align: left;
                                                  padding-bottom: 20px;
                                                  padding-left: 30px;
                                                  padding-right: 10px;
                                                  padding-top: 20px;
                                                  vertical-align: middle;
                                                  border-top: 0px;
                                                  border-right: 0px;
                                                  border-bottom: 0px;
                                                  border-left: 0px;
                                                "
                                                width="33.333333333333336%"
                                              >
                                                <table
                                                  border="0"
                                                  cellpadding="0"
                                                  cellspacing="0"
                                                  class="image_block block-1"
                                                  role="presentation"
                                                  style="
                                                    mso-table-lspace: 0pt;
                                                    mso-table-rspace: 0pt;
                                                  "
                                                  width="100%"
                                                >
                                                  <tr>
                                                    <td
                                                      class="pad"
                                                      style="
                                                        width: 100%;
                                                        padding-right: 0px;
                                                        padding-left: 0px;
                                                      "
                                                    >
                                                      <div
                                                        align="center"
                                                        class="alignment"
                                                        style="line-height: 10px"
                                                      >
                                                        <a
                                                          href="https://www.example.com"
                                                          style="outline: none"
                                                          tabindex="-1"
                                                          target="_blank"
                                                          ><img
                                                            alt="your logo"
                                                          src="http://gcc.iq/assets/images/logo.jpeg"
                                                            style="
                                                              display: block;
                                                              border: 0;
                                                              border-radius: 50%;
                                                            "
                                                            title="your logo"
                                                            width="50"
                                                            height="50"
                                                        /></a>
                                                      </div>
                                                    </td>
                                                  </tr>
                                                </table>
                                              </td>
                                              <td
                                                class="column column-2"
                                                style="
                                                  mso-table-lspace: 0pt;
                                                  mso-table-rspace: 0pt;
                                                  font-weight: 400;
                                                  text-align: left;
                                                  padding-bottom: 5px;
                                                  padding-left: 25px;
                                                  padding-right: 30px;
                                                  padding-top: 5px;
                                                  vertical-align: middle;
                                                  border-top: 0px;
                                                  border-right: 0px;
                                                  border-bottom: 0px;
                                                  border-left: 0px;
                                                "
                                                width="66.66666666666667%"
                                              >
                                                <table
                                                  border="0"
                                                  cellpadding="0"
                                                  cellspacing="0"
                                                  class="paragraph_block block-1"
                                                  role="presentation"
                                                  style="
                                                    mso-table-lspace: 0pt;
                                                    mso-table-rspace: 0pt;
                                                    word-break: break-word;
                                                  "
                                                  width="100%"
                                                >
                                                  <tr>
                                                    <td class="pad">
                                                      <div
                                                        style="
                                                          color: #ffffff;
                                                          direction: ltr;
                                                          font-family: Inter, sans-serif;
                                                          font-size: 14px;
                                                          font-weight: 400;
                                                          letter-spacing: 0px;
                                                          line-height: 120%;
                                                          text-align: right;
                                                          mso-line-height-alt: 16.8px;
                                                        "
                                                      >
                                                        <p style="margin: 0">
                                                          Copyright © 2023 Golden Castel, All
                                                          rights reserved.
                                                        </p>
                                                      </div>
                                                    </td>
                                                  </tr>
                                                </table>
                                              </td>
                                            </tr>
                                          </tbody>
                                        </table>
                                      </td>
                                    </tr>
                                  </tbody>
                                </table>
                                <table
                                  align="center"
                                  border="0"
                                  cellpadding="0"
                                  cellspacing="0"
                                  class="row row-15"
                                  role="presentation"
                                  style="mso-table-lspace: 0pt; mso-table-rspace: 0pt"
                                  width="100%"
                                >
                                  <tbody>
                                    <tr>
                                      <td>
                                        <table
                                          align="center"
                                          border="0"
                                          cellpadding="0"
                                          cellspacing="0"
                                          class="row-content stack"
                                          role="presentation"
                                          style="
                                            mso-table-lspace: 0pt;
                                            mso-table-rspace: 0pt;
                                            color: #000;
                                            width: 700px;
                                            margin: 0 auto;
                                          "
                                          width="700"
                                        >
                                          <tbody>
                                            <tr>
                                              <td
                                                class="column column-1"
                                                style="
                                                  mso-table-lspace: 0pt;
                                                  mso-table-rspace: 0pt;
                                                  font-weight: 400;
                                                  text-align: left;
                                                  padding-bottom: 5px;
                                                  padding-top: 5px;
                                                  vertical-align: top;
                                                  border-top: 0px;
                                                  border-right: 0px;
                                                  border-bottom: 0px;
                                                  border-left: 0px;
                                                "
                                                width="100%"
                                              >
                                                <table
                                                  border="0"
                                                  cellpadding="0"
                                                  cellspacing="0"
                                                  class="icons_block block-1"
                                                  role="presentation"
                                                  style="
                                                    mso-table-lspace: 0pt;
                                                    mso-table-rspace: 0pt;
                                                  "
                                                  width="100%"
                                                >
                                                  <tr>
                                                    <td
                                                      class="pad"
                                                      style="
                                                        vertical-align: middle;
                                                        color: #9d9d9d;
                                                        font-family: inherit;
                                                        font-size: 15px;
                                                        padding-bottom: 5px;
                                                        padding-top: 5px;
                                                        text-align: center;
                                                      "
                                                    >
                                                      <table
                                                        cellpadding="0"
                                                        cellspacing="0"
                                                        role="presentation"
                                                        style="
                                                          mso-table-lspace: 0pt;
                                                          mso-table-rspace: 0pt;
                                                        "
                                                        width="100%"
                                                      >
                                                        <tr>
                                                          <td
                                                            class="alignment"
                                                            style="
                                                              vertical-align: middle;
                                                              text-align: center;
                                                            "
                                                          >
                                                            <!--[if vml]><table align="left" cellpadding="0" cellspacing="0" role="presentation" style="display:inline-block;padding-left:0px;padding-right:0px;mso-table-lspace: 0pt;mso-table-rspace: 0pt;"><![endif]-->
                                                            <!--[if !vml]><!-->
                                                            <table
                                                              cellpadding="0"
                                                              cellspacing="0"
                                                              class="icons-inner"
                                                              role="presentation"
                                                              style="
                                                                mso-table-lspace: 0pt;
                                                                mso-table-rspace: 0pt;
                                                                display: inline-block;
                                                                margin-right: -4px;
                                                                padding-left: 0px;
                                                                padding-right: 0px;
                                                              "
                                                            >
                                                              <!--<![endif]-->
                                                              <tr></tr>
                                                            </table>
                                                          </td>
                                                        </tr>
                                                      </table>
                                                    </td>
                                                  </tr>
                                                </table>
                                              </td>
                                            </tr>
                                          </tbody>
                                        </table>
                                      </td>
                                    </tr>
                                  </tbody>
                                </table>
                              </td>
                            </tr>
                          </tbody>
                        </table>
                        <!-- End -->
                      </body>
                    </html>';
        // Send the email
        $mail->send();
    } catch (Exception $e) {
        echo $e;
    }
    echo json_encode(array("status" => "success", "data" => "Success"));

}
