

Queries
```
query SpecialSomeone {
  specialSomeonesForUser(
    order: {
      firstName: ASC
      lastName: ASC
    }
  ) {
    id
    uniqueIdentifier
    firstName
    lastName
    nickname
    user {
      firstName
      lastName
    }
    notes {
      message
    }
  }
}

query notes {
  notes(
    where: {
      specialSomeone: {
        uniqueIdentifier: { eq: "wfyK!bTi.AOtqL7rrzawlXwn_Ah60G9K@3hGdaDkrxiek"}
      }
      and: [
        {
          createdUTC: { gte: "2023-08-01" }
        },
        {
          createdUTC: { lte: "2023-08-31" }
        }
      ]
    }
    order: {
      createdUTC: ASC
    }
  ){
    totalCount
    nodes{
     id
     message
     createdUTC
     specialSomeone{
      firstName
      lastName
     }
     user{
      firstName
      lastName
     }
    }
    pageInfo{
      hasNextPage
      hasPreviousPage
      startCursor
      endCursor
    }
    edges{
      cursor
      node{
        message
      }
    }
  }
}

query notesCountOnly {
  notes(
    where: {
      specialSomeone: {
        uniqueIdentifier: { eq: "wfyK!bTi.AOtqL7rrzawlXwn_Ah60G9K@3hGdaDkrxiek"}
      }
    }
  ){
    totalCount
  }
}
```

Mutations
```
mutation createSS {
    createSpecialSomeone(
        input: {
            firstName: "XXX"
            lastName: "Silva"
            nickName: "My <3"
        }
    ) {
      specialSomeone {
        id
        uniqueIdentifier
        firstName
        lastName
        nickname
        user {
          id
          emailAddress
          firstName
          lastName
        }
        notes {
          message
        }
      }
    }
}

mutation createNote {
  createNote(
    input: {
      message: "yyyySSSS"
      specialSomeoneId: "U3BlY2lhbFNvbWVvbmUKaTU5"
    }
  ) {
    note {
      id
      createdUTC
      message
      user {
        firstName
        lastName
      }
      specialSomeone {
        firstName
        lastName
        nickname
      }
    }
  }
}

```
