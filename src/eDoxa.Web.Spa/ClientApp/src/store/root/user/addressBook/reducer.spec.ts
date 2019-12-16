import { reducer, initialState } from "./reducer";
import { AxiosError } from "axios";
import {
  LOAD_USER_ADDRESSBOOK_SUCCESS,
  DELETE_USER_ADDRESS_SUCCESS,
  CREATE_USER_ADDRESS_SUCCESS,
  UPDATE_USER_ADDRESS_SUCCESS,
  LOAD_USER_ADDRESSBOOK_FAIL
} from "store/actions/identity/types";

const addressId = "test_id";
const addressBook204Data = [];
const addressBook200Data = [{ id: "1" }, { id: "2" }, { id: "1" }];

const removeSuccessData = { addressId: "1" };
const removeExpectedState = initialState.data.filter(
  address => address.id !== addressId
);

describe("user address book reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_ADDRESS_BOOK_SUCCESS 204", () => {
    const action: any = {
      type: LOAD_USER_ADDRESSBOOK_SUCCESS,
      payload: { status: 204, data: addressBook204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_ADDRESS_BOOK_SUCCESS 200", () => {
    const action: any = {
      type: LOAD_USER_ADDRESSBOOK_SUCCESS,
      payload: { status: 200, data: addressBook200Data }
    };
    const state = {
      data: addressBook200Data,
      error: null,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });

  it("should handle REMOVE_ADDRESS_SUCCESS", () => {
    const action: any = {
      type: DELETE_USER_ADDRESS_SUCCESS,
      payload: { data: removeSuccessData }
    };
    const state = {
      data: removeExpectedState,
      error: null,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });

  it("should handle ADD_ADDRESS_SUCCESS", () => {
    const action: any = {
      type: CREATE_USER_ADDRESS_SUCCESS
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle UPDATE_ADDRESS_SUCCESS", () => {
    const action: any = {
      type: UPDATE_USER_ADDRESS_SUCCESS
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_ADDRESS_BOOK_FAIL", () => {
    const error: AxiosError = {
      isAxiosError: true,
      config: {},
      name: "",
      message: ""
    };
    const action: any = {
      type: LOAD_USER_ADDRESSBOOK_FAIL,
      error
    };
    const state = {
      data: initialState.data,
      error,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });
});
