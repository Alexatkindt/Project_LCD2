#include <rgb_lcd.h>

rgb_lcd lcd;

void setup() {
  Serial.begin(9600);   //baud rate gelijk zetten aan WPF
  lcd.begin(16, 2);     //RGB initialiseren
}

void loop() {
  if (Serial.available() > 0) {
    String command = Serial.readStringUntil('\n');

    if (command.equals("Hello, Arduino!"))      //als de ontvangen string gelijk is aan "Hello, Arduino!" dan
    {

      lcd.setRGB(0, 100, 100);                  //LCd op cyan/appelblauw zeegroen zetten
      lcd.print("Hello, Arduino!");             //Hello arduino tonen

      Serial.print("Received Command: ");
      Serial.println(command);
    }
     else if (command.equals("reset")) {        //anders ontvangen string gelijk is aan 'reset"
      lcd.clear();  
      Serial.println("");                       //clear LCD 
      lcd.setRGB(34, 139, 34);                  //LCd op groen zetten
    }
  }
}