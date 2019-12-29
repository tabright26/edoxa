import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";
import Profile from "./Profile";
import { StripeBankAccountState } from "store/root/payment/stripe/bankAccount/types";
import { StripePaymentMethodsState } from "store/root/payment/stripe/paymentMethod/types";
import { UserAccountTransactionsState } from "store/root/user/account/transactions/types";
import { GameAccountCredentialState } from "store/root/user/game/credential/types";
import { UserAddressBookState } from "store/root/user/addressBook/types";
import { UserEmailState } from "store/root/user/email/types";
import { UserProfileState } from "store/root/user/profile/types";
import { UserPhoneState } from "store/root/user/phone/types";

it("renders without crashing", () => {
  //Arrange
  const paymentMethods: StripePaymentMethodsState = {
    data: {
      object: "list",
      data: [
        {
          type: "card",
          object: "payment_method",
          metadata: {},
          livemode: false,
          id: "testID",
          customer: "testCustomer",
          card: {
            brand: "visa",
            checks: {
              address_line1_check: "pass",
              address_postal_code_check: "pass",
              cvc_check: "pass"
            },
            country: "CA",
            exp_month: 11,
            exp_year: 22,
            fingerprint: "test",
            funding: "credit",
            generated_from: null,
            last4: "4242",
            three_d_secure_usage: { supported: true },
            wallet: null
          },
          billing_details: {
            address: { line1: "test address" },
            email: "gabriel@edoxa.gg",
            name: "Gabriel Roy",
            phone: "123456789"
          },
          created: 11111111
        }
      ],
      has_more: false,
      url: "testURL"
    },
    loading: false,
    error: null
  };

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

  const transactions: UserAccountTransactionsState = {
    data: [
      {
        timestamp: 111111,
        currency: "money",
        amount: 100,
        description: "test",
        type: "charge",
        status: "succeded",
        id: "1"
      },
      {
        timestamp: 222222,
        currency: "money",
        amount: 100,
        description: "test",
        type: "deposit",
        status: "succeded",
        id: "2"
      },
      {
        timestamp: 333333,
        currency: "money",
        amount: 100,
        description: "test",
        type: "withdrawal",
        status: "succeded",
        id: "3"
      }
    ],
    loading: false,
    error: null
  };

  const games: GameAccountCredentialState = {
    data: [{ name: "League of legends", id: "accountID" }],
    loading: false,
    error: null
  };

  const addressBook: UserAddressBookState = {
    data: [
      {
        country: "Canada",
        line1: "test address",
        city: "Montreal",
        id: "1"
      },
      {
        country: "Canada",
        line1: "test address",
        city: "Quebec",
        id: "2"
      }
    ],
    loading: false,
    error: null
  };

  const email: UserEmailState = {
    data: { address: "gabriel@edoxa.gg", verified: true },
    loading: false,
    error: null
  };

  const informations: UserProfileState = {
    data: {
      firstName: "Gabriel",
      lastName: "Roy",
      gender: "Male",
      dob: { year: 1995, month: 8, day: 4 }
    },
    loading: false,
    error: null
  };

  const phone: UserPhoneState = {
    data: { number: "123456789", verified: true },
    loading: false,
    error: null
  };

  const store: any = {
    getState: () => {
      return {
        root: {
          user: {
            transactions,
            games,
            addressBook,
            email,
            informations,
            phone
          },
          payment: {
            stripe: {
              bankAccount,
              paymentMethods
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
        <MemoryRouter>
          <Profile
            match={{ params: "userId", isExact: false, path: "", url: "" }}
            history={null}
            location={null}
          />
        </MemoryRouter>
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
