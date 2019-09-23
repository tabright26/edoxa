import { loadAddressBook, addAddress, updateAddress, removeAddress } from "./actions";
import {
  LOAD_ADDRESS_BOOK,
  LOAD_ADDRESS_BOOK_SUCCESS,
  LOAD_ADDRESS_BOOK_FAIL,
  ADD_ADDRESS,
  ADD_ADDRESS_SUCCESS,
  ADD_ADDRESS_FAIL,
  REMOVE_ADDRESS,
  REMOVE_ADDRESS_SUCCESS,
  REMOVE_ADDRESS_FAIL,
  UPDATE_ADDRESS,
  UPDATE_ADDRESS_SUCCESS,
  UPDATE_ADDRESS_FAIL
} from "./types";

describe("identity actions", () => {
  it("should create an action to get user address book", () => {
    const expectedType = [LOAD_ADDRESS_BOOK, LOAD_ADDRESS_BOOK_SUCCESS, LOAD_ADDRESS_BOOK_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = "/identity/api/address-book";

    const object = loadAddressBook();

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to post user address book", () => {
    const expectedType = [ADD_ADDRESS, ADD_ADDRESS_SUCCESS, ADD_ADDRESS_FAIL];
    const expectedMethod = "POST";
    const expectedUrl = "/identity/api/address-book";
    const expectedAddress = { country: "Canada", city: "Montreal" };

    const object = addAddress(expectedAddress);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
    expect(object.payload.request.data).toEqual(expectedAddress);
  });

  it("should create an action to put user address book", () => {
    const addressId = "1";

    const expectedType = [UPDATE_ADDRESS, UPDATE_ADDRESS_SUCCESS, UPDATE_ADDRESS_FAIL];
    const expectedMethod = "PUT";
    const expectedUrl = `/identity/api/address-book/${addressId}`;
    const expectedAddress = { country: "Canada", city: "Montreal" };

    const object = updateAddress(addressId, expectedAddress);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
    expect(object.payload.request.data).toEqual(expectedAddress);
  });

  it("should create an action to remove user address book", () => {
    const addressId = "1";

    const expectedType = [REMOVE_ADDRESS, REMOVE_ADDRESS_SUCCESS, REMOVE_ADDRESS_FAIL];
    const expectedMethod = "DELETE";
    const expectedUrl = `/identity/api/address-book/${addressId}`;

    const object = removeAddress(addressId);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to remove user address book", () => {
    const addressId = "1";
    const expectedType = [REMOVE_ADDRESS, REMOVE_ADDRESS_SUCCESS, REMOVE_ADDRESS_FAIL];
    const expectedMethod = "DELETE";
    const expectedUrl = `/identity/api/address-book/${addressId}`;

    const object = removeAddress(addressId);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });
});
