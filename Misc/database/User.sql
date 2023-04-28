CREATE TABLE "public"."User" (
    "UserId" int GENERATED ALWAYS AS IDENTITY,
    "FirstName" varchar(50) NOT NULL,
    "LastName" varchar(50) NOT NULL,
    "EmailAddress" varchar(100) NOT NULL,
    "SignedUpUTC" timestamp without time zone NOT NULL DEFAULT (now() at time zone 'utc'),
    PRIMARY KEY ("UserId")
);
