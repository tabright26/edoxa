import { reducer, initialState } from "./reducer";

const addressId = "test_id";
const addressBook204Data = [];
const addressBook200Data = [{ id: "1" }, { id: "2" }, { id: "1" }];

const removeSuccessData = { addressId: "1" };
const removeExpectedState = initialState.filter(address => address.id !== addressId);

describe("user address book reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_ADDRESS_BOOK_SUCCESS 204", () => {
    const action: any = {
      type: "LOAD_ADDRESS_BOOK_SUCCESS",
      payload: { status: 204, data: addressBook204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_ADDRESS_BOOK_SUCCESS 200", () => {
    const action: any = {
      type: "LOAD_ADDRESS_BOOK_SUCCESS",
      payload: { status: 200, data: addressBook200Data }
    };
    expect(reducer(initialState, action)).toEqual(addressBook200Data);
  });

  it("should handle REMOVE_ADDRESS_SUCCESS", () => {
    const action: any = {
      type: "REMOVE_ADDRESS_SUCCESS",
      payload: { data: removeSuccessData }
    };
    expect(reducer(initialState, action)).toEqual(removeExpectedState);
  });

  // it("should handle ADD_ADDRESS_FAIL", () => {
  //   const action: any = {
  //     type: "ADD_ADDRESS_FAIL",
  //     error: axiosFailErrorData
  //   };
  //   expect(reducer(initialState, action)).toEqual(initialState);
  // });

  // it("should handle UPDATE_ADDRESS_FAIL", () => {
  //   const action: any = {
  //     type: "UPDATE_ADDRESS_FAIL",
  //     error: axiosFailErrorData
  //   };
  //   expect(reducer(initialState, action)).toEqual(initialState);
  // });

  // it("should handle REMOVE_ADDRESS_FAIL", () => {
  //   const action: any = {
  //     type: "REMOVE_ADDRESS_FAIL",
  //     error: axiosFailErrorData
  //   };
  //   expect(reducer(initialState, action)).toEqual(initialState);
  // });

  it("should handle ADD_ADDRESS_SUCCESS", () => {
    const action: any = {
      type: "ADD_ADDRESS_SUCCESS"
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle UPDATE_ADDRESS_SUCCESS", () => {
    const action: any = {
      type: "UPDATE_ADDRESS_SUCCESS"
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_ADDRESS_BOOK_FAIL", () => {
    const action: any = {
      type: "LOAD_ADDRESS_BOOK_FAIL"
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });
});
