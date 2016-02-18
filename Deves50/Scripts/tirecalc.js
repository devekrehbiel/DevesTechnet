function compute1(form)
{
   if(form.tr.value < .01 || form.tr.value > 99.9)
   {
      form.tr.value = 1.00;
   }

   if(form.tc.value < .01 || form.tc.value > 99.9)
   {
      form.tc.value = 1.00;
   }

   if(form.ar.value < 1.00 || form.ar.value > 9.99)
   {
      form.ar.value = 0.00;
   }

   if(form.mp.value < 1 || form.mp.value > 300)
   {
      form.mp.value = 0;
   }

   if(form.td.value < 10 || form.td.value > 99)
   {
      form.td.value = 0;
   }

   form.rp.value = (form.mp.value * form.ar.value * form.tr.value * form.tc.value * 336) / form.td.value;

   if(form.mp.value == 0 && form.ar.value == 0 && form.td.value == 0)
   {
      form.rp.value = "N/A";
   }

   return;
}

function compute2(form)
{
   if(form.nt.value < 10 || form.nt.value > 99)
   {
      form.nt.value = 0;
   }

   if(form.td.value < 10 || form.td.value > 99)
   {
      form.td.value = 0;
   }

   if(form.ar.value < 1.00 || form.ar.value > 9.99)
   {
      form.ar.value = 0.00;
   }

   form.na.value = (form.nt.value / form.td.value) * form.ar.value;

   if(form.nt.value == 0 && form.td.value == 0 && form.ar.value == 0)
   {
      form.na.value = "N/A";
   }

   return;
}

function compute3(form)
{
   if(form.tr.value < .01 || form.tr.value > 99.9)
   {
      form.tr.value = 1.00;
   }

   if(form.tc.value < .01 || form.tc.value > 99.9)
   {
      form.tc.value = 1.00;
   }

   if(form.ar.value < 1.00 || form.ar.value > 9.99)
   {
      form.ar.value = 0;
   }

   if(form.rp.value < 1 || form.rp.value > 99999)
   {
      form.rp.value = 0;
   }

   if(form.td.value < 10 || form.td.value > 99)
   {
      form.td.value = 0;
   }

   form.mp.value = (form.rp.value * form.td.value) / (form.ar.value * form.tr.value * form.tc.value * 336);

   if(form.rp.value == 0 && form.ar.value == 0 && form.td.value == 0)
   {
      form.mp.value = "N/A";
   }

   return;
}

function compute4(form)
{
   if(form.tr.value < .01 || form.tr.value > 99.9)
   {
      form.tr.value = 1.00;
   }

   if(form.tc.value < .01 || form.tc.value > 99.9)
   {
      form.tc.value = 1.00;
   }

   if(form.ar.value < 1.00 || form.ar.value > 9.99)
   {
      form.ar.value = 0;
   }

   form.or.value = form.tr.value * form.tc.value * form.ar.value;

   if(form.ar.value == 0)
   {
      form.or.value = "N/A";
   }

   return;
}

function compute5(form)
{
   if(form.sw.value < 50 || form.sw.value > 499)
   {
      form.sw.value = 0;
   }

   if(form.as.value < 10 | form.as.value > 99)
   {
      form.as.value = 0;
   }

   if(form.rim.value < 12 || form.rim.value > 19)
   {
      form.rim.value = 0;
   }

   form.td.value = ((form.sw.value * form.as.value * 2) / 2540) + parseInt(form.rim.value);

   if(form.sw.value == 0 || form.as.value == 0 || form.rim.value == 0)
   {
      form.td.value = "N/A";
   }

   return;
}

function compute6(form)
{
   if(form.ntd.value < 1 || form.ntd.value > 499)
   {
      form.ntd.value = 0;
   }

   if(form.otd.value < 1 || form.otd.value > 599)
   {
      form.otd.value = 0;
   }

   if(form.sv.value < 1 || form.sv.value > 200)
   {
      form.sv.value = 0;
   }

   form.ns.value = (form.ntd.value / form.otd.value) * form.sv.value;

   return;
}

function clear1(form)
{
   form.mp.value = "";
   form.ar.value = "";
   form.tr.value = "";
   form.tc.value = "";
   form.rp.value = "";
   form.td.value = "";
}

function clear2(form)
{
   form.nt.value = "";
   form.td.value = "";
   form.ar.value = "";
   form.na.value = "";
}

function clear3(form)
{
   form.mp.value = "";
   form.ar.value = "";
   form.tr.value = "";
   form.tc.value = "";
   form.rp.value = "";
   form.td.value = "";
}

function clear4(form)
{
   form.tr.value = "";
   form.tc.value = "";
   form.ar.value = "";
   form.or.value = "";
}

function clear5(form)
{
   form.sw.value = "";
   form.as.value = "";
   form.rim.value = "";
   form.td.value = "";
}

function clear6(form)
{
   form.ntd.value = "";
   form.otd.value = "";
   form.sv.value = "";
   form.ns.value = "";
}