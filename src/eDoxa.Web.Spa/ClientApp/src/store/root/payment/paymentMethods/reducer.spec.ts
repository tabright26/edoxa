import { reducer, initialState } from "./reducer";

const stripeCard204Data = [];
const stripeCard200Data = { data: [{ id: "1" }] };

describe("user account stripe card reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_PAYMENTMETHODS_SUCCESS 204", () => {
    const action: any = {
      type: "LOAD_PAYMENTMETHODS_SUCCESS",
      payload: { status: 204, data: stripeCard204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_PAYMENTMETHODS_SUCCESS 200", () => {
    const action: any = {
      type: "LOAD_PAYMENTMETHODS_SUCCESS",
      payload: { status: 200, data: stripeCard200Data }
    };
    const state = {
      data: stripeCard200Data,
      error: null,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });

  it("should handle LOAD_PAYMENTMETHODS_FAIL", () => {
    const action: any = {
      type: "LOAD_PAYMENTMETHODS_FAIL"
    };
    const state = {
      data: initialState.data,
      error: "LOAD_PAYMENTMETHODS_FAIL",
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });
});
