<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <title>OWL - Contact Us</title>
  <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
  <script src="bootstrap/js/bootstrap.min.js"></script>
  <script data-main="src/main" src="lib/require.js"></script>
  <link rel="stylesheet" href="main.css">
</head>

<body>
  <!-- OWL ICON -->
  <div id="page-header">
    <div id="navigation-bar">
      <nav class="navbar navbar-inverse">
        <div class="container-fluid">
          <!-- Brand and toggle get grouped for better mobile display -->
          <div class="navbar-header">
            <a class="navbar-brand" href="#">
              <img src="./pictures/owl_library_small.png" alt="OWL Mascot" style="width:33px;height:42px;position:absolute;top: 10px;left:22px;"
              class="img-rounded">
            </a>
          </div>

          <!-- Collect the nav links, forms, and other content for toggling -->
          <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
            <ul class="nav navbar-nav">
              <li><a href="./About.html">About<span class="sr-only">(current)</span></a></li>
              <li><a href="./ContactUs.html">Contact</a></li>
            </ul>
            <ul class="nav navbar-nav navbar-right">
              <li><a href="./OWL.html">Home</a></li>
              <li><a href="./OWLAdmin.html">Admin Login</a></li>
            </ul>
          </div><!-- /.navbar-collapse -->
        </div><!-- /.container-fluid -->
      </nav>
    </div>

    <div class="row">
      <div class="col-xs-1 col-sm-1"></div>
      <div class="col-xs-3 col-sm-2">
        <a href="./OWL.html">
          <img src="./pictures/owl_library_small.png" alt="OWL Mascot" style="width:120px;height:150px" class="img-rounded">
        </a>
      </div>

      <!-- Banner in the background -->
      <div class="container">
        <div id="banner" class="jumbotron">
          <a href="./OWL.html" id="jumbotron">
            <h1>Online Wildlife Library</h1>
          </a>
          <h2>Try Our Interactive Identifier</h2>
          <h2>to Discover and Learn about Plants</h2>
        </div>     
      </div>
    </div>
    <!-- MISSING big plants search icon -->
    <!-- <img src="./pictures/tree_dummy.png" alt="Plant Search" style="width:171px;height:171px" class="img-rounded"> -->

    <!-- MISSING big animals search icon -->
    <!-- <img src="./pictures/owl.jpg" alt="Plant Search" style="width:171px;height:171px" class="img-rounded"> -->

  </div>

  <div class="container-fluid">
    <div id="ContactUsEmailSent">
      <div class="row">
        <div class="col-xs-10 col-xs-offset-1">
			<%
Dim Mail, BUFromEmail, BUPassword
Set Mail = CreateObject("CDO.Message")
BUFromEmail = "wguan12@bu.edu"
BUPassword  = ""

Mail.Configuration.Fields.Item ("http://schemas.microsoft.com/cdo/configuration/sendusing") = 2
Mail.Configuration.Fields.Item ("http://schemas.microsoft.com/cdo/configuration/smtpserver") ="smtp.bu.edu"
Mail.Configuration.Fields.Item ("http://schemas.microsoft.com/cdo/configuration/smtpserverport") = 465

Mail.Configuration.Fields.Item ("http://schemas.microsoft.com/cdo/configuration/smtpusessl") = 1

Mail.Configuration.Fields.Item ("http://schemas.microsoft.com/cdo/configuration/smtpconnectiontimeout") = 60

Mail.Configuration.Fields.Item ("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate") = 1
Mail.Configuration.Fields.Item ("http://schemas.microsoft.com/cdo/configuration/sendusername") = BUFromEmail
Mail.Configuration.Fields.Item ("http://schemas.microsoft.com/cdo/configuration/sendpassword") = BUPassword

Mail.Configuration.Fields.Update

Mail.Subject="OWL Project - Report a Bug Email - " & Request.QueryString("subject")
Mail.From="wguan12@bu.edu"
Mail.To="wguan12@bu.edu"
Mail.TextBody=Request.QueryString("message")

Mail.Send
Set Mail = Nothing
			%>

			<h2>Thank you for your feedback!  An email has been sent! <br> We will have a member of our team review your feedback.</h2>
			<img class="img-responsive" src="./pictures/landscape-background1.jpg" style="width:1020px;height:685px" class="img-rounded">
			<img class="img-responsive"src="./pictures/landscape-background1.jpg" style="width:1020px;height:685px" class="img-rounded">
        </div>
      </div>
    </div>
    <br>
    <div id="footer" class="row">
      <br/>
      <div id="footer1" class="col-sm-3">
        <img src="./pictures/owl_library_small.png" alt="OWL Mascot" style="width:90px;height:114px">
      </div>

      <div id="footer2" class="col-sm-2">
        <p>General</p>
        <p><a href="./About.html">About</a></p>
        <p><a href="./QuickLinks.html">Quick Links</a></p>
        <p><a href="./FAQs.html">FAQs</a></p>
      </div>

      <div id="footer3" class="col-sm-2">
        <p>Contact</p>
        <p><a href="./ContactUs.html">Contact us</a></p>
        <p><a href="./ReportBug.html">Report a Bug</a></p>
      </div>

      <div id="footer4" class="col-xs-3">
        <p>Follow</p>
        <a href="https://www.facebook.com/USDA/?fref=ts"><img src="./pictures/LOGOS/Facebook/logos-and-badges_f-logo_online/png/FB-f-Logo__blue_50.png" alt="FB Logo" style="width:57px;height:57px" class="img-rounded"></a>
        <a href="https://instagram.com/usdagov/"><img src="./pictures/LOGOS/Instagram/1Multi-ColorLogo_online/Instagram_Icon_Large.png" alt="Instagram Logo" style="width:60px;height:60px" class="img-rounded"></a>
        <a href="https://twitter.com/USDA?ref_src=twsrc%5Egoogle%7Ctwcamp%5Eserp%7Ctwgr%5Eauthor"><img src="./pictures/LOGOS/Twitter/brand guideline logos/TwitterLogo_55acee.png" alt="Twitter Logo" style="width:62px;height:62px" class="img-rounded"></a>

      </div>

      <div id="admin" class="col-sm-1">
        <div class="btn-group" role="group" aria-label="...">
          <button type="button" class="btn btn-default">Admin Login</button>
        </div>
      </div>

    </div>
  </div>

</body>
</html>
