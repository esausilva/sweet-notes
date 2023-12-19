/* eslint-disable */
import { TypedDocumentNode as DocumentNode } from '@graphql-typed-document-node/core';
export type Maybe<T> = T | null;
export type InputMaybe<T> = Maybe<T>;
export type Exact<T extends { [key: string]: unknown }> = { [K in keyof T]: T[K] };
export type MakeOptional<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]?: Maybe<T[SubKey]> };
export type MakeMaybe<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]: Maybe<T[SubKey]> };
export type MakeEmpty<T extends { [key: string]: unknown }, K extends keyof T> = { [_ in K]?: never };
export type Incremental<T> = T | { [P in keyof T]?: P extends ' $fragmentName' | '__typename' ? T[P] : never };
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: { input: string; output: string; }
  String: { input: string; output: string; }
  Boolean: { input: boolean; output: boolean; }
  Int: { input: number; output: number; }
  Float: { input: number; output: number; }
  /** The `DateTime` scalar represents an ISO-8601 compliant date time type. */
  DateTime: { input: any; output: any; }
};

export enum ApplyPolicy {
  AfterResolver = 'AFTER_RESOLVER',
  BeforeResolver = 'BEFORE_RESOLVER',
  Validation = 'VALIDATION'
}

export type CreateNoteCommandInput = {
  message: Scalars['String']['input'];
  specialSomeoneId: Scalars['ID']['input'];
};

export type CreateNotePayload = {
  __typename?: 'CreateNotePayload';
  errors?: Maybe<Array<UserError>>;
  note: Note;
};

export type CreateSpecialSomeoneCommandInput = {
  firstName: Scalars['String']['input'];
  lastName: Scalars['String']['input'];
  nickName?: InputMaybe<Scalars['String']['input']>;
};

export type CreateSpecialSomeonePayload = {
  __typename?: 'CreateSpecialSomeonePayload';
  errors?: Maybe<Array<UserError>>;
  specialSomeone: SpecialSomeone;
};

export type DateTimeOperationFilterInput = {
  eq?: InputMaybe<Scalars['DateTime']['input']>;
  gt?: InputMaybe<Scalars['DateTime']['input']>;
  gte?: InputMaybe<Scalars['DateTime']['input']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['DateTime']['input']>>>;
  lt?: InputMaybe<Scalars['DateTime']['input']>;
  lte?: InputMaybe<Scalars['DateTime']['input']>;
  neq?: InputMaybe<Scalars['DateTime']['input']>;
  ngt?: InputMaybe<Scalars['DateTime']['input']>;
  ngte?: InputMaybe<Scalars['DateTime']['input']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['DateTime']['input']>>>;
  nlt?: InputMaybe<Scalars['DateTime']['input']>;
  nlte?: InputMaybe<Scalars['DateTime']['input']>;
};

export type Mutation = {
  __typename?: 'Mutation';
  createNote: CreateNotePayload;
  createSpecialSomeone: CreateSpecialSomeonePayload;
};


export type MutationCreateNoteArgs = {
  input: CreateNoteCommandInput;
};


export type MutationCreateSpecialSomeoneArgs = {
  input: CreateSpecialSomeoneCommandInput;
};

/** The node interface is implemented by entities that have a global unique identifier. */
export type Node = {
  id: Scalars['ID']['output'];
};

export type Note = Node & {
  __typename?: 'Note';
  createdUTC: Scalars['DateTime']['output'];
  id: Scalars['ID']['output'];
  message: Scalars['String']['output'];
  specialSomeone: SpecialSomeone;
  user: User;
};

export type NoteFilterInput = {
  and?: InputMaybe<Array<NoteFilterInput>>;
  createdUTC?: InputMaybe<DateTimeOperationFilterInput>;
  message?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<NoteFilterInput>>;
  specialSomeone?: InputMaybe<SpecialSomeoneFilterInput>;
};

export type NoteSortInput = {
  createdUTC?: InputMaybe<SortEnumType>;
};

/** A connection to a list of items. */
export type NotesConnection = {
  __typename?: 'NotesConnection';
  /** A list of edges. */
  edges?: Maybe<Array<NotesEdge>>;
  /** A flattened list of the nodes. */
  nodes?: Maybe<Array<Note>>;
  /** Information to aid in pagination. */
  pageInfo: PageInfo;
  /** Identifies the total count of items in the connection. */
  totalCount: Scalars['Int']['output'];
};

/** An edge in a connection. */
export type NotesEdge = {
  __typename?: 'NotesEdge';
  /** A cursor for use in pagination. */
  cursor: Scalars['String']['output'];
  /** The item at the end of the edge. */
  node: Note;
};

/** Information about pagination in a connection. */
export type PageInfo = {
  __typename?: 'PageInfo';
  /** When paginating forwards, the cursor to continue. */
  endCursor?: Maybe<Scalars['String']['output']>;
  /** Indicates whether more edges exist following the set defined by the clients arguments. */
  hasNextPage: Scalars['Boolean']['output'];
  /** Indicates whether more edges exist prior the set defined by the clients arguments. */
  hasPreviousPage: Scalars['Boolean']['output'];
  /** When paginating backwards, the cursor to continue. */
  startCursor?: Maybe<Scalars['String']['output']>;
};

export type Query = {
  __typename?: 'Query';
  /** Fetches an object given its ID. */
  node?: Maybe<Node>;
  /** Lookup nodes by a list of IDs. */
  nodes: Array<Maybe<Node>>;
  notes?: Maybe<NotesConnection>;
  specialSomeonesForUser: Array<SpecialSomeone>;
};


export type QueryNodeArgs = {
  id: Scalars['ID']['input'];
};


export type QueryNodesArgs = {
  ids: Array<Scalars['ID']['input']>;
};


export type QueryNotesArgs = {
  after?: InputMaybe<Scalars['String']['input']>;
  before?: InputMaybe<Scalars['String']['input']>;
  first?: InputMaybe<Scalars['Int']['input']>;
  last?: InputMaybe<Scalars['Int']['input']>;
  order?: InputMaybe<Array<NoteSortInput>>;
  where?: InputMaybe<NoteFilterInput>;
};


export type QuerySpecialSomeonesForUserArgs = {
  order?: InputMaybe<Array<SpecialSomeoneSortInput>>;
};

export enum SortEnumType {
  Asc = 'ASC',
  Desc = 'DESC'
}

export type SpecialSomeone = Node & {
  __typename?: 'SpecialSomeone';
  firstName: Scalars['String']['output'];
  id: Scalars['ID']['output'];
  lastName: Scalars['String']['output'];
  nickname?: Maybe<Scalars['String']['output']>;
  notes: Array<Note>;
  uniqueIdentifier: Scalars['String']['output'];
  user: User;
};

export type SpecialSomeoneFilterInput = {
  and?: InputMaybe<Array<SpecialSomeoneFilterInput>>;
  or?: InputMaybe<Array<SpecialSomeoneFilterInput>>;
  uniqueIdentifier?: InputMaybe<StringOperationFilterInput>;
};

export type SpecialSomeoneSortInput = {
  firstName?: InputMaybe<SortEnumType>;
  lastName?: InputMaybe<SortEnumType>;
};

export type StringOperationFilterInput = {
  and?: InputMaybe<Array<StringOperationFilterInput>>;
  contains?: InputMaybe<Scalars['String']['input']>;
  endsWith?: InputMaybe<Scalars['String']['input']>;
  eq?: InputMaybe<Scalars['String']['input']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['String']['input']>>>;
  ncontains?: InputMaybe<Scalars['String']['input']>;
  nendsWith?: InputMaybe<Scalars['String']['input']>;
  neq?: InputMaybe<Scalars['String']['input']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['String']['input']>>>;
  nstartsWith?: InputMaybe<Scalars['String']['input']>;
  or?: InputMaybe<Array<StringOperationFilterInput>>;
  startsWith?: InputMaybe<Scalars['String']['input']>;
};

export type User = {
  __typename?: 'User';
  emailAddress: Scalars['String']['output'];
  firstName: Scalars['String']['output'];
  id: Scalars['Int']['output'];
  lastName: Scalars['String']['output'];
  notes: Array<Note>;
  specialSomeones: Array<SpecialSomeone>;
};

export type UserError = {
  __typename?: 'UserError';
  code: Scalars['String']['output'];
  message: Scalars['String']['output'];
};

export type SpecialSomeoneQueryVariables = Exact<{ [key: string]: never; }>;


export type SpecialSomeoneQuery = { __typename?: 'Query', specialSomeonesForUser: Array<{ __typename?: 'SpecialSomeone', id: string, uniqueIdentifier: string, firstName: string, lastName: string, nickname?: string | null }> };

export type CreateNoteMutationVariables = Exact<{
  message: Scalars['String']['input'];
  specialSomeoneId: Scalars['ID']['input'];
}>;


export type CreateNoteMutation = { __typename?: 'Mutation', createNote: { __typename?: 'CreateNotePayload', note: { __typename?: 'Note', id: string } } };


export const SpecialSomeoneDocument = {"kind":"Document","definitions":[{"kind":"OperationDefinition","operation":"query","name":{"kind":"Name","value":"SpecialSomeone"},"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"specialSomeonesForUser"},"arguments":[{"kind":"Argument","name":{"kind":"Name","value":"order"},"value":{"kind":"ObjectValue","fields":[{"kind":"ObjectField","name":{"kind":"Name","value":"firstName"},"value":{"kind":"EnumValue","value":"ASC"}},{"kind":"ObjectField","name":{"kind":"Name","value":"lastName"},"value":{"kind":"EnumValue","value":"ASC"}}]}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"id"}},{"kind":"Field","name":{"kind":"Name","value":"uniqueIdentifier"}},{"kind":"Field","name":{"kind":"Name","value":"firstName"}},{"kind":"Field","name":{"kind":"Name","value":"lastName"}},{"kind":"Field","name":{"kind":"Name","value":"nickname"}}]}}]}}]} as unknown as DocumentNode<SpecialSomeoneQuery, SpecialSomeoneQueryVariables>;
export const CreateNoteDocument = {"kind":"Document","definitions":[{"kind":"OperationDefinition","operation":"mutation","name":{"kind":"Name","value":"createNote"},"variableDefinitions":[{"kind":"VariableDefinition","variable":{"kind":"Variable","name":{"kind":"Name","value":"message"}},"type":{"kind":"NonNullType","type":{"kind":"NamedType","name":{"kind":"Name","value":"String"}}}},{"kind":"VariableDefinition","variable":{"kind":"Variable","name":{"kind":"Name","value":"specialSomeoneId"}},"type":{"kind":"NonNullType","type":{"kind":"NamedType","name":{"kind":"Name","value":"ID"}}}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"createNote"},"arguments":[{"kind":"Argument","name":{"kind":"Name","value":"input"},"value":{"kind":"ObjectValue","fields":[{"kind":"ObjectField","name":{"kind":"Name","value":"message"},"value":{"kind":"Variable","name":{"kind":"Name","value":"message"}}},{"kind":"ObjectField","name":{"kind":"Name","value":"specialSomeoneId"},"value":{"kind":"Variable","name":{"kind":"Name","value":"specialSomeoneId"}}}]}}],"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"note"},"selectionSet":{"kind":"SelectionSet","selections":[{"kind":"Field","name":{"kind":"Name","value":"id"}}]}}]}}]}}]} as unknown as DocumentNode<CreateNoteMutation, CreateNoteMutationVariables>;