CREATE TABLE "public"."Note" (
    "NoteId" int GENERATED ALWAYS AS IDENTITY,
    "UserId" int NOT NULL,
    "SpecialSomeoneId" int NOT NULL,
    "Message" varchar(150) NOT NULL,
    "CreatedUTC" timestamp without time zone NOT NULL DEFAULT (now() at time zone 'utc'),
    PRIMARY KEY ("NoteId"),
    CONSTRAINT "FK_Note_User" FOREIGN KEY ("UserId") REFERENCES "public"."User"("UserId"),
    CONSTRAINT "FK_Note_SpecialSomeone" FOREIGN KEY ("SpecialSomeoneId") REFERENCES "public"."SpecialSomeone"("SpecialSomeoneId")
);

CREATE UNIQUE INDEX "IX_Note_Unique_CreatedUTC" ON "public"."Note" ("CreatedUTC" DESC);
