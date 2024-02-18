export type GraphQLErrorResponse = {
  response: GraphQLError;
};

export type GraphQLError = {
  errors: GraphQLErrorObject[];
};

type GraphQLErrorObject = {
  message: string;
};
