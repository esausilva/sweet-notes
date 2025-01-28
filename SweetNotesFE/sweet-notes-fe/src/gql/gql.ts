/* eslint-disable */
import * as types from './graphql';
import { TypedDocumentNode as DocumentNode } from '@graphql-typed-document-node/core';

/**
 * Map of all GraphQL operations in the project.
 *
 * This map has several performance disadvantages:
 * 1. It is not tree-shakeable, so it will include all operations in the project.
 * 2. It is not minifiable, so the string of a GraphQL query will be multiple times inside the bundle.
 * 3. It does not support dead code elimination, so it will add unused operations.
 *
 * Therefore it is highly recommended to use the babel or swc plugin for production.
 * Learn more about it here: https://the-guild.dev/graphql/codegen/plugins/presets/preset-client#reducing-bundle-size
 */
const documents = {
    "\n  mutation createSpecialSomeone(\n    $firstName: String!\n    $nickName: String\n    $lastName: String!\n  ) {\n    createSpecialSomeone(\n      input: { firstName: $firstName, lastName: $lastName, nickName: $nickName }\n    ) {\n      specialSomeone {\n        id\n      }\n    }\n  }\n": types.CreateSpecialSomeoneDocument,
    "\n  mutation deleteNote($id: ID!) {\n    deleteNote(input: { id: $id }) {\n      note {\n        id\n      }\n    }\n  }\n": types.DeleteNoteDocument,
    "\n  query NotesAdmin(\n    $uniqueIdentifier: String!\n    $from: DateTime!\n    $to: DateTime!\n  ) {\n    notes(\n      where: {\n        specialSomeone: { uniqueIdentifier: { eq: $uniqueIdentifier } }\n        and: [{ createdUTC: { gte: $from } }, { createdUTC: { lte: $to } }]\n      }\n      order: { createdUTC: DESC }\n    ) {\n      totalCount\n      nodes {\n        message\n        id\n        createdUTC\n      }\n    }\n  }\n": types.NotesAdminDocument,
    "\n  mutation createNote($message: String!, $specialSomeoneId: ID!) {\n    createNote(\n      input: { message: $message, specialSomeoneId: $specialSomeoneId }\n    ) {\n      note {\n        id\n      }\n    }\n  }\n": types.CreateNoteDocument,
    "\n  query NotesSlug(\n    $uniqueIdentifier: String!\n    $from: DateTime!\n    $to: DateTime!\n  ) {\n    notes(\n      where: {\n        specialSomeone: { uniqueIdentifier: { eq: $uniqueIdentifier } }\n        and: [{ createdUTC: { gte: $from } }, { createdUTC: { lte: $to } }]\n      }\n      order: { createdUTC: DESC }\n    ) {\n      totalCount\n      nodes {\n        message\n        createdUTC\n      }\n    }\n  }\n": types.NotesSlugDocument,
    "\n  query SpecialSomeone {\n    specialSomeonesForUser(order: { firstName: ASC, lastName: ASC }) {\n      id\n      uniqueIdentifier\n      firstName\n      lastName\n      nickname\n    }\n  }\n": types.SpecialSomeoneDocument,
};

/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 *
 *
 * @example
 * ```ts
 * const query = graphql(`query GetUser($id: ID!) { user(id: $id) { name } }`);
 * ```
 *
 * The query argument is unknown!
 * Please regenerate the types.
 */
export function graphql(source: string): unknown;

/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "\n  mutation createSpecialSomeone(\n    $firstName: String!\n    $nickName: String\n    $lastName: String!\n  ) {\n    createSpecialSomeone(\n      input: { firstName: $firstName, lastName: $lastName, nickName: $nickName }\n    ) {\n      specialSomeone {\n        id\n      }\n    }\n  }\n"): (typeof documents)["\n  mutation createSpecialSomeone(\n    $firstName: String!\n    $nickName: String\n    $lastName: String!\n  ) {\n    createSpecialSomeone(\n      input: { firstName: $firstName, lastName: $lastName, nickName: $nickName }\n    ) {\n      specialSomeone {\n        id\n      }\n    }\n  }\n"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "\n  mutation deleteNote($id: ID!) {\n    deleteNote(input: { id: $id }) {\n      note {\n        id\n      }\n    }\n  }\n"): (typeof documents)["\n  mutation deleteNote($id: ID!) {\n    deleteNote(input: { id: $id }) {\n      note {\n        id\n      }\n    }\n  }\n"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "\n  query NotesAdmin(\n    $uniqueIdentifier: String!\n    $from: DateTime!\n    $to: DateTime!\n  ) {\n    notes(\n      where: {\n        specialSomeone: { uniqueIdentifier: { eq: $uniqueIdentifier } }\n        and: [{ createdUTC: { gte: $from } }, { createdUTC: { lte: $to } }]\n      }\n      order: { createdUTC: DESC }\n    ) {\n      totalCount\n      nodes {\n        message\n        id\n        createdUTC\n      }\n    }\n  }\n"): (typeof documents)["\n  query NotesAdmin(\n    $uniqueIdentifier: String!\n    $from: DateTime!\n    $to: DateTime!\n  ) {\n    notes(\n      where: {\n        specialSomeone: { uniqueIdentifier: { eq: $uniqueIdentifier } }\n        and: [{ createdUTC: { gte: $from } }, { createdUTC: { lte: $to } }]\n      }\n      order: { createdUTC: DESC }\n    ) {\n      totalCount\n      nodes {\n        message\n        id\n        createdUTC\n      }\n    }\n  }\n"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "\n  mutation createNote($message: String!, $specialSomeoneId: ID!) {\n    createNote(\n      input: { message: $message, specialSomeoneId: $specialSomeoneId }\n    ) {\n      note {\n        id\n      }\n    }\n  }\n"): (typeof documents)["\n  mutation createNote($message: String!, $specialSomeoneId: ID!) {\n    createNote(\n      input: { message: $message, specialSomeoneId: $specialSomeoneId }\n    ) {\n      note {\n        id\n      }\n    }\n  }\n"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "\n  query NotesSlug(\n    $uniqueIdentifier: String!\n    $from: DateTime!\n    $to: DateTime!\n  ) {\n    notes(\n      where: {\n        specialSomeone: { uniqueIdentifier: { eq: $uniqueIdentifier } }\n        and: [{ createdUTC: { gte: $from } }, { createdUTC: { lte: $to } }]\n      }\n      order: { createdUTC: DESC }\n    ) {\n      totalCount\n      nodes {\n        message\n        createdUTC\n      }\n    }\n  }\n"): (typeof documents)["\n  query NotesSlug(\n    $uniqueIdentifier: String!\n    $from: DateTime!\n    $to: DateTime!\n  ) {\n    notes(\n      where: {\n        specialSomeone: { uniqueIdentifier: { eq: $uniqueIdentifier } }\n        and: [{ createdUTC: { gte: $from } }, { createdUTC: { lte: $to } }]\n      }\n      order: { createdUTC: DESC }\n    ) {\n      totalCount\n      nodes {\n        message\n        createdUTC\n      }\n    }\n  }\n"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "\n  query SpecialSomeone {\n    specialSomeonesForUser(order: { firstName: ASC, lastName: ASC }) {\n      id\n      uniqueIdentifier\n      firstName\n      lastName\n      nickname\n    }\n  }\n"): (typeof documents)["\n  query SpecialSomeone {\n    specialSomeonesForUser(order: { firstName: ASC, lastName: ASC }) {\n      id\n      uniqueIdentifier\n      firstName\n      lastName\n      nickname\n    }\n  }\n"];

export function graphql(source: string) {
  return (documents as any)[source] ?? {};
}

export type DocumentType<TDocumentNode extends DocumentNode<any, any>> = TDocumentNode extends DocumentNode<  infer TType,  any>  ? TType  : never;