import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import BankAccount from "./BankAccount";
import { StripeBankAccountState } from "store/root/payment/stripe/bankAccount/types";

it("renders without crashing", () => {
  //Arrange
  const bankAccount: StripeBankAccountState = {
    data: {
      account: null,
      default_for_currency: true,
      metadata: {},
      object: "bank_account",
      account_holder_name: "Test Name",
      account_holder_type: "individual",
      bank_name: "RBC",
      country: "Canada",
      currency: "CAD",
      fingerprint: "Test",
      last4: "4242",
      routing_number: "1234567890",
      status: "verified",
      id: "accountID"
    },
    loading: false,
    error: null
  };

  const store: any = {
    getState: () => {
      return {
        root: {
          payment: {
            stripe: {
              bankAccount
            }
          }
        }
      };
    },
    dispatch: action => {},
    subscribe: () => {}
  };

  //Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <BankAccount />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
