import {
  loadUserAddressBook,
  createUserAddress,
  updateUserAddress,
  deleteUserAddress,
  forgotUserPassword,
  resetUserPassword,
  loadUserDoxatagHistory,
  updateUserDoxatag,
  confirmUserEmail,
  loadUserInformations,
  createUserInformations,
  updateUserInformations
} from "./actions";

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
  FORGOT_USER_PASSWORD,
  FORGOT_USER_PASSWORD_SUCCESS,
  FORGOT_USER_PASSWORD_FAIL,
  RESET_USER_PASSWORD,
  RESET_USER_PASSWORD_SUCCESS,
  RESET_USER_PASSWORD_FAIL,
  LOAD_USER_DOXATAGHISTORY,
  LOAD_USER_DOXATAGHISTORY_SUCCESS,
  LOAD_USER_DOXATAGHISTORY_FAIL,
  UPDATE_USER_DOXATAG,
  UPDATE_USER_DOXATAG_SUCCESS,
  UPDATE_USER_DOXATAG_FAIL,
  CONFIRM_USER_EMAIL,
  CONFIRM_USER_EMAIL_SUCCESS,
  CONFIRM_USER_EMAIL_FAIL,
  LOAD_USER_INFORMATIONS,
  LOAD_USER_INFORMATIONS_SUCCESS,
  LOAD_USER_INFORMATIONS_FAIL,
  CREATE_USER_INFORMATIONS,
  CREATE_USER_INFORMATIONS_SUCCESS,
  CREATE_USER_INFORMATIONS_FAIL,
  UPDATE_USER_INFORMATIONS,
  UPDATE_USER_INFORMATIONS_SUCCESS,
  UPDATE_USER_INFORMATIONS_FAIL
} from "./types";

describe("identity actions", () => {
  it("should create an action to get user address book", () => {
    const expectedType = [
      LOAD_USER_ADDRESSBOOK,
      LOAD_USER_ADDRESSBOOK_SUCCESS,
      LOAD_USER_ADDRESSBOOK_FAIL
    ];
    const expectedMethod = "GET";
    const expectedUrl = "/identity/api/address-book";

    const object = loadUserAddressBook();

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to post user address book", () => {
    const expectedType = [
      CREATE_USER_ADDRESS,
      CREATE_USER_ADDRESS_SUCCESS,
      CREATE_USER_ADDRESS_FAIL
    ];
    const expectedMethod = "POST";
    const expectedUrl = "/identity/api/address-book";
    const expectedAddress = { country: "Canada", city: "Montreal" };

    const object = createUserAddress(expectedAddress, null);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
    expect(object.payload.request.data).toEqual(expectedAddress);
  });

  it("should create an action to put user address book", () => {
    const addressId = "1";

    const expectedType = [
      UPDATE_USER_ADDRESS,
      UPDATE_USER_ADDRESS_SUCCESS,
      UPDATE_USER_ADDRESS_FAIL
    ];
    const expectedMethod = "PUT";
    const expectedUrl = `/identity/api/address-book/${addressId}`;
    const expectedAddress = { country: "Canada", city: "Montreal" };

    const object = updateUserAddress(addressId, expectedAddress, null);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
    expect(object.payload.request.data).toEqual(expectedAddress);
  });

  it("should create an action to remove user address book", () => {
    const addressId = "1";

    const expectedType = [
      DELETE_USER_ADDRESS,
      DELETE_USER_ADDRESS_SUCCESS,
      DELETE_USER_ADDRESS_FAIL
    ];
    const expectedMethod = "DELETE";
    const expectedUrl = `/identity/api/address-book/${addressId}`;

    const object = deleteUserAddress(addressId, null);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to remove user address book", () => {
    const addressId = "1";
    const expectedType = [
      DELETE_USER_ADDRESS,
      DELETE_USER_ADDRESS_SUCCESS,
      DELETE_USER_ADDRESS_FAIL
    ];
    const expectedMethod = "DELETE";
    const expectedUrl = `/identity/api/address-book/${addressId}`;

    const object = deleteUserAddress(addressId, null);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });
});

describe("identity actions", () => {
  it("should create an action to get user doxatag history", () => {
    const expectedType = [
      LOAD_USER_DOXATAGHISTORY,
      LOAD_USER_DOXATAGHISTORY_SUCCESS,
      LOAD_USER_DOXATAGHISTORY_FAIL
    ];
    const expectedMethod = "GET";
    const expectedUrl = "/identity/api/doxatag-history";

    const actionCreator = loadUserDoxatagHistory();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to post user doxatag", () => {
    const expectedType = [
      UPDATE_USER_DOXATAG,
      UPDATE_USER_DOXATAG_SUCCESS,
      UPDATE_USER_DOXATAG_FAIL
    ];
    const expectedMethod = "POST";
    const expectedUrl = "/identity/api/doxatag-history";
    const expectedDoxatag = "Doxatag";

    const actionCreator = updateUserDoxatag(expectedDoxatag, null);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
    expect(actionCreator.payload.request.data).toEqual(expectedDoxatag);
  });
});

describe("identity actions", () => {
  it("should create an action to get confirm email", () => {
    const userId = "1";
    const code = "0";
    const expectedType = [
      CONFIRM_USER_EMAIL,
      CONFIRM_USER_EMAIL_SUCCESS,
      CONFIRM_USER_EMAIL_FAIL
    ];
    const expectedMethod = "GET";
    const expectedUrl = `/identity/api/email/confirm?userId=${userId}&code=${code}`;

    const object = confirmUserEmail(userId, code);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });
});

describe("identity actions", () => {
  it("should create an action to get user personal info", () => {
    const expectedType = [
      LOAD_USER_INFORMATIONS,
      LOAD_USER_INFORMATIONS_SUCCESS,
      LOAD_USER_INFORMATIONS_FAIL
    ];
    const expectedMethod = "GET";
    const expectedUrl = "/identity/api/informations";

    const actionCreator = loadUserInformations();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to post user personal info", () => {
    const expectedType = [
      CREATE_USER_INFORMATIONS,
      CREATE_USER_INFORMATIONS_SUCCESS,
      CREATE_USER_INFORMATIONS_FAIL
    ];
    const expectedMethod = "POST";
    const expectedUrl = "/identity/api/informations";
    const expectedInformations = {
      firstName: "Bob",
      lastName: "Afrete",
      gender: "Male",
      dob: { year: 1990, month: 5, day: 10 }
    };

    const object = createUserInformations(expectedInformations, null);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
    expect(object.payload.request.data).toEqual(expectedInformations);
  });

  it("should create an action to put user personal info", () => {
    const expectedType = [
      UPDATE_USER_INFORMATIONS,
      UPDATE_USER_INFORMATIONS_SUCCESS,
      UPDATE_USER_INFORMATIONS_FAIL
    ];
    const expectedMethod = "PUT";
    const expectedUrl = "/identity/api/informations";
    const expectedInformations = { firstName: "Bob" };

    const object = updateUserInformations(expectedInformations, null);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
    expect(object.payload.request.data).toEqual(expectedInformations);
  });
});

describe("identity actions", () => {
  it("should create an action to post user forgot password", () => {
    const expectedType = [
      FORGOT_USER_PASSWORD,
      FORGOT_USER_PASSWORD_SUCCESS,
      FORGOT_USER_PASSWORD_FAIL
    ];
    const expectedMethod = "POST";
    const expectedUrl = "/identity/api/password/forgot";
    const expectedEmail = { email: "gab@edoxa.gg" };

    const object = forgotUserPassword(expectedEmail, null);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
    expect(object.payload.request.data).toEqual(expectedEmail);
  });

  it("should create an action to post user reset password", () => {
    const expectedType = [
      RESET_USER_PASSWORD,
      RESET_USER_PASSWORD_SUCCESS,
      RESET_USER_PASSWORD_FAIL
    ];
    const expectedMethod = "POST";
    const expectedUrl = "/identity/api/password/reset";
    const expectedEmail = { email: "gab@edoxa.gg", password: "password" };

    const object = resetUserPassword(expectedEmail, null);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
    expect(object.payload.request.data).toEqual(expectedEmail);
  });
});
