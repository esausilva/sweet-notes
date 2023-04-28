CREATE TABLE "public"."SpecialSomeone" (
    "SpecialSomeoneId" int GENERATED ALWAYS AS IDENTITY,
    "UserId" int NOT NULL,
    "UniqueIdentifier" varchar(128) NOT NULL,
    "FirstName" varchar(50) NOT NULL,
    "LastName" varchar(50) NOT NULL,
    "Nickname" varchar(50),
    PRIMARY KEY ("SpecialSomeoneId"),
    CONSTRAINT "FK_SpecialSomone_User" FOREIGN KEY ("UserId") REFERENCES "public"."User"("UserId")
);
