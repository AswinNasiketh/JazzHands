#include <PID_v1.h>
#include <SerialCommand.h>
#include <Servo.h>

#define PIN_INPUT 0
#define PIN_OUTPUT 3
#define BACKWARD_SPEED_LIMIT 70
#define FORWARD_SPEED_LIMIT 110

SerialCommand sCmd;
double currentFingerPos[5]; 
double nextFingerPos[5];
float tempFloat[5];


double Kp[] = {2,2,2,2,2};
double Ki[] = {5,5,5,5,5};
double Kd[] = {1,1,1,1,1};
double servoCommand[] = {90, 90, 90, 90, 90};
//PID pid0(&currentFingerPos[0], &servoCommand[0], &nextFingerPos[0], Kp[0],Ki[0],Kd[0], DIRECT); 
PID pid1(&currentFingerPos[1], &servoCommand[1], &nextFingerPos[1], Kp[1],Ki[1],Kd[1], DIRECT);
//PID pid2(&currentFingerPos[2], &servoCommand[2], &nextFingerPos[2], Kp[2],Ki[2],Kd[2], DIRECT);
//PID pid3(&currentFingerPos[3], &servoCommand[3], &nextFingerPos[3], Kp[3],Ki[3],Kd[3], DIRECT);
//PID pid4(&currentFingerPos[4], &servoCommand[4], &nextFingerPos[4], Kp[4],Ki[4],Kd[4], DIRECT);

int servoPins[] = {7,8,9,10,11};
Servo servos[5];

void setup() {
  Serial.begin(9600);
  while(!Serial);

  sCmd.addCommand("READ", readHandler);
  sCmd.addCommand("SET", setHandler);
  
  pinMode(A0, INPUT);
  pinMode(A1, INPUT);
  pinMode(A2, INPUT);
  pinMode(A3, INPUT);
  pinMode(A4, INPUT);


 // pid0.SetMode(AUTOMATIC);
 // pid0.SetOutputLimits(BACKWARD_SPEED_LIMIT, FORWARD_SPEED_LIMIT);
  pid1.SetMode(AUTOMATIC);
  pid1.SetOutputLimits(BACKWARD_SPEED_LIMIT, FORWARD_SPEED_LIMIT);
 // pid2.SetMode(AUTOMATIC);
//  pid2.SetOutputLimits(BACKWARD_SPEED_LIMIT, FORWARD_SPEED_LIMIT);
 // pid3.SetMode(AUTOMATIC);
//  pid3.SetOutputLimits(BACKWARD_SPEED_LIMIT, FORWARD_SPEED_LIMIT);
//  pid4.SetMode(AUTOMATIC);
//  pid4.SetOutputLimits(BACKWARD_SPEED_LIMIT, FORWARD_SPEED_LIMIT);

  for(int i = 0; i < 5; i++){
    servos[i].attach(servoPins[i]);
  }
}

void loop() {

    currentFingerPos[1] = (double) stretch(1,tempFloat[1]);
  
//
 updateServos();
//  Serial.println(tempFloat[1]);
  
  if (Serial.available() > 0){
  sCmd.readSerial();
  }
}

void setHandler(const char *command){
  char *arg;
  for(int i = 0; i < 5; i++){
    if(arg != NULL){
      arg = sCmd.next(); 
    nextFingerPos[i] = (double) atoi(arg);
    }else{
      nextFingerPos[i] = 0;
    }    
  }  
}

void readHandler(const char *command){
  for(int i =0; i< 5; i++){
    Serial.println((int)currentFingerPos[i]);
  }
} 

double stretch(int inPin, float &out){
 out += (analogRead(inPin) - out) * 0.2;
 double mapped = map(out, 850, 900, 0, 90);
 if(mapped > 90){
   mapped = 90;
 }
 if(mapped < 0){
   mapped = 0;
 }
 return mapped;
}

void updateServos(){
  //pid0.Compute();
  pid1.Compute();
 // pid2.Compute();
  //pid3.Compute();
 // pid4.Compute();
  for (int i =0; i < 5; i++){
    servos[i].write((int)servoCommand[i]);
  }
}

