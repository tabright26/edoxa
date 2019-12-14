import {
  LOAD_USER_ADDRESSBOOK,
  LOAD_USER_ADDRESSBOOK_SUCCESS,
  LOAD_USER_ADDRESSBOOK_FAIL,
  CREATE_USER_ADDRESS,
  CREATE_USER_ADDRESS_SUCCESS,
  CREATE_USER_ADDRESS_FAIL,
  DELETE_USER_ADDRESS,
  DELETE_USER_ADDRESS_SUCCESS,
  DELETE_USER_ADDRESS_FAIL,
  UPDATE_USER_ADDRESS,
  UPDATE_USER_ADDRESS_SUCCESS,
  UPDATE_USER_ADDRESS_FAIL,
  LOAD_USER_DOXATAGHISTORY,
  LOAD_USER_DOXATAGHISTORY_SUCCESS,
  LOAD_USER_DOXATAGHISTORY_FAIL,
  UPDATE_USER_DOXATAG,
  UPDATE_USER_DOXATAG_SUCCESS,
  UPDATE_USER_DOXATAG_FAIL,
  LOAD_USER_INFORMATIONS,
  LOAD_USER_INFORMATIONS_SUCCESS,
  LOAD_USER_INFORMATIONS_FAIL,
  CREATE_USER_INFORMATIONS,
  CREATE_USER_INFORMATIONS_SUCCESS,
  CREATE_USER_INFORMATIONS_FAIL,
  UPDATE_USER_INFORMATIONS,
  UPDATE_USER_INFORMATIONS_SUCCESS,
  UPDATE_USER_INFORMATIONS_FAIL,
  GENERATE_GAME_AUTHENTICATION,
  GENERATE_GAME_AUTHENTICATION_SUCCESS,
  GENERATE_GAME_AUTHENTICATION_FAIL,
  VALIDATE_GAME_AUTHENTICATION,
  VALIDATE_GAME_AUTHENTICATION_SUCCESS,
  VALIDATE_GAME_AUTHENTICATION_FAIL,
  UNLINK_GAME_CREDENTIAL,
  UNLINK_GAME_CREDENTIAL_SUCCESS,
  UNLINK_GAME_CREDENTIAL_FAIL,
  FORGOT_USER_PASSWORD,
  FORGOT_USER_PASSWORD_SUCCESS,
  FORGOT_USER_PASSWORD_FAIL,
  RESET_USER_PASSWORD,
  RESET_USER_PASSWORD_SUCCESS,
  RESET_USER_PASSWORD_FAIL,
  LOAD_USER_PHONE,
  LOAD_USER_PHONE_SUCCESS,
  LOAD_USER_PHONE_FAIL,
  UPDATE_USER_PHONE,
  UPDATE_USER_PHONE_SUCCESS,
  UPDATE_USER_PHONE_FAIL,
  LOAD_USER_EMAIL,
  LOAD_USER_EMAIL_SUCCESS,
  LOAD_USER_EMAIL_FAIL,
  CONFIRM_USER_EMAIL,
  CONFIRM_USER_EMAIL_SUCCESS,
  CONFIRM_USER_EMAIL_FAIL,
  UserInformationsActionCreators,
  UserDoxatagHistoryActionCreators,
  UserAddressBookActionCreators,
  UserPhoneActionCreators,
  UserEmailActionCreators,
  UserPasswordActionCreators,
  GameAuthenticationActionCreators,
  GameCredentialActionCreators
} from "./types";

import { Game, AddressId } from "types";
import { AxiosActionCreatorMeta } from "utils/axios/types";

export function loadUserAddressBook(): UserAddressBookActionCreators {
  return {
    types: [
      LOAD_USER_ADDRESSBOOK,
      LOAD_USER_ADDRESSBOOK_SUCCESS,
      LOAD_USER_ADDRESSBOOK_FAIL
    ],
    payload: {
      request: {
        method: "GET",
        url: `/identity/api/address-book`
      }
    }
  };
}

export function createUserAddress(
  data: any,
  meta: AxiosActionCreatorMeta
): UserAddressBookActionCreators {
  return {
    types: [
      CREATE_USER_ADDRESS,
      CREATE_USER_ADDRESS_SUCCESS,
      CREATE_USER_ADDRESS_FAIL
    ],
    payload: {
      request: {
        method: "POST",
        url: "/identity/api/address-book",
        data
      }
    },
    meta
  };
}

export function updateUserAddress(
  addressId: AddressId,
  data: any,
  meta: AxiosActionCreatorMeta
): UserAddressBookActionCreators {
  return {
    types: [
      UPDATE_USER_ADDRESS,
      UPDATE_USER_ADDRESS_SUCCESS,
      UPDATE_USER_ADDRESS_FAIL
    ],
    payload: {
      request: {
        method: "PUT",
        url: `/identity/api/address-book/${addressId}`,
        data
      }
    },
    meta
  };
}

export function deleteUserAddress(
  addressId: AddressId,
  meta: AxiosActionCreatorMeta
): UserAddressBookActionCreators {
  return {
    types: [
      DELETE_USER_ADDRESS,
      DELETE_USER_ADDRESS_SUCCESS,
      DELETE_USER_ADDRESS_FAIL
    ],
    payload: {
      request: {
        method: "DELETE",
        url: `/identity/api/address-book/${addressId}`
      }
    },
    meta
  };
}

export function loadUserDoxatagHistory(): UserDoxatagHistoryActionCreators {
  return {
    types: [
      LOAD_USER_DOXATAGHISTORY,
      LOAD_USER_DOXATAGHISTORY_SUCCESS,
      LOAD_USER_DOXATAGHISTORY_FAIL
    ],
    payload: {
      request: {
        method: "GET",
        url: "/identity/api/doxatag-history"
      }
    }
  };
}

export function updateUserDoxatag(
  data: any,
  meta: AxiosActionCreatorMeta
): UserDoxatagHistoryActionCreators {
  return {
    types: [
      UPDATE_USER_DOXATAG,
      UPDATE_USER_DOXATAG_SUCCESS,
      UPDATE_USER_DOXATAG_FAIL
    ],
    payload: {
      request: {
        method: "POST",
        url: "/identity/api/doxatag-history",
        data
      }
    },
    meta
  };
}

export function loadUserEmail(): UserEmailActionCreators {
  return {
    types: [LOAD_USER_EMAIL, LOAD_USER_EMAIL_SUCCESS, LOAD_USER_EMAIL_FAIL],
    payload: {
      request: {
        method: "GET",
        url: "/identity/api/email"
      }
    }
  };
}

export function confirmUserEmail(
  userId: string,
  code: string
): UserEmailActionCreators {
  return {
    types: [
      CONFIRM_USER_EMAIL,
      CONFIRM_USER_EMAIL_SUCCESS,
      CONFIRM_USER_EMAIL_FAIL
    ],
    payload: {
      request: {
        method: "GET",
        url: `/identity/api/email/confirm?userId=${userId}&code=${code}`
      }
    }
  };
}

export function generateGameAuthentication(
  game: Game,
  data: any
): GameAuthenticationActionCreators {
  return {
    types: [
      GENERATE_GAME_AUTHENTICATION,
      GENERATE_GAME_AUTHENTICATION_SUCCESS,
      GENERATE_GAME_AUTHENTICATION_FAIL
    ],
    payload: {
      request: {
        method: "POST",
        url: `games/api/games/${game}/authentications`,
        data
      }
    }
  };
}

export function validateGameAuthentication(
  game: Game
): GameAuthenticationActionCreators {
  return {
    types: [
      VALIDATE_GAME_AUTHENTICATION,
      VALIDATE_GAME_AUTHENTICATION_SUCCESS,
      VALIDATE_GAME_AUTHENTICATION_FAIL
    ],
    payload: {
      request: {
        method: "PUT",
        url: `games/api/games/${game}/authentications`
      }
    }
  };
}

export function unlinkGameCredential(game: Game): GameCredentialActionCreators {
  return {
    types: [
      UNLINK_GAME_CREDENTIAL,
      UNLINK_GAME_CREDENTIAL_SUCCESS,
      UNLINK_GAME_CREDENTIAL_FAIL
    ],
    payload: {
      request: {
        method: "DELETE",
        url: `games/api/games/${game}/credentials`
      }
    }
  };
}

export function loadUserInformations(): UserInformationsActionCreators {
  return {
    types: [
      LOAD_USER_INFORMATIONS,
      LOAD_USER_INFORMATIONS_SUCCESS,
      LOAD_USER_INFORMATIONS_FAIL
    ],
    payload: {
      request: {
        method: "GET",
        url: "/identity/api/informations"
      }
    }
  };
}

export function createUserInformations(
  data: any,
  meta: AxiosActionCreatorMeta
): UserInformationsActionCreators | any {
  return {
    types: [
      CREATE_USER_INFORMATIONS,
      CREATE_USER_INFORMATIONS_SUCCESS,
      CREATE_USER_INFORMATIONS_FAIL
    ],
    payload: {
      request: {
        method: "POST",
        url: "/identity/api/informations",
        data
      }
    },
    meta
  };
}

export function updateUserInformations(
  data: any,
  meta: AxiosActionCreatorMeta
): UserInformationsActionCreators {
  return {
    types: [
      UPDATE_USER_INFORMATIONS,
      UPDATE_USER_INFORMATIONS_SUCCESS,
      UPDATE_USER_INFORMATIONS_FAIL
    ],
    payload: {
      request: {
        method: "PUT",
        url: "/identity/api/informations",
        data: {
          firstName: data.firstName
        }
      }
    },
    meta
  };
}

export function forgotUserPassword(
  data: any,
  meta: AxiosActionCreatorMeta
): UserPasswordActionCreators {
  return {
    types: [
      FORGOT_USER_PASSWORD,
      FORGOT_USER_PASSWORD_SUCCESS,
      FORGOT_USER_PASSWORD_FAIL
    ],
    payload: {
      request: {
        method: "POST",
        url: "/identity/api/password/forgot",
        data
      }
    },
    meta
  };
}

export function resetUserPassword(
  data: any,
  meta: AxiosActionCreatorMeta
): UserPasswordActionCreators {
  return {
    types: [
      RESET_USER_PASSWORD,
      RESET_USER_PASSWORD_SUCCESS,
      RESET_USER_PASSWORD_FAIL
    ],
    payload: {
      request: {
        method: "POST",
        url: "/identity/api/password/reset",
        data
      }
    },
    meta
  };
}

export function loadUserPhone(): UserPhoneActionCreators {
  return {
    types: [LOAD_USER_PHONE, LOAD_USER_PHONE_SUCCESS, LOAD_USER_PHONE_FAIL],
    payload: {
      request: {
        method: "GET",
        url: "/identity/api/phone"
      }
    }
  };
}

export function updateUserPhone(
  data: any,
  meta: AxiosActionCreatorMeta
): UserPhoneActionCreators {
  return {
    types: [
      UPDATE_USER_PHONE,
      UPDATE_USER_PHONE_SUCCESS,
      UPDATE_USER_PHONE_FAIL
    ],
    payload: {
      request: {
        method: "POST",
        url: "/identity/api/phone",
        data
      }
    },
    meta
  };
}
