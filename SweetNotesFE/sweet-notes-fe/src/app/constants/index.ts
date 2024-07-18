export const AUTH_COOKIE_NAME = 'SweetNotesAuthCookie';

export enum Routes {
  SIGNUP = 'signup',
  USER = 'user',
  USER_ADMINISTRATION = 'user/administration',
  USER_SIGNUP = 'user/signup',
  USER_LOGIN = 'user/login',
  USER_ME = 'user/me',
  SPECIAL_SOMEONE = 'ss',
  SPECIAL_SOMEONE_NAME = 'special-someone-name',
}

export enum QueryKeys {
  SPECIAL_SOMEONES = 'specialSomeones',
  SPECIAL_SOMEONE_NOTES = 'specialSomeoneNotes',
}

export enum MutationKeys {
  CREATE_NOTE = 'createNote',
  CREATE_SPECIAL_SOMEONE = 'createSpecialSomeone',
  LOGIN = 'login',
  SIGNUP = 'signup',
}

export enum ApiRootUris {
  FOR_CLIENT_SIDE,
  FOR_SERVER_SIDE,
}
