import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";
import Profile from ".";
import store from "store";

it("renders without crashing", () => {
  // Arrange
  // const paymentMethods: StripePaymentMethodsState = {
  //   data: [
  //     {
  //       id: "0",
  //       card: {
  //         brand: "visa",
  //         country: "CA",
  //         expMonth: 6,
  //         expYear: 2015,
  //         last4: "4242"
  //       }
  //     }
  //   ],
  //   loading: false,
  //   error: null
  // };

  // const bankAccount: StripeBankAccountState = {
  //   data: {
  //     bankName: "RBC",
  //     country: "Canada",
  //     currency: "CAD",
  //     last4: "4242",
  //     status: "verified",
  //     defaultForCurrency: true
  //   },
  //   loading: false,
  //   error: null
  // };

  // const transactions: UserTransactionState = {
  //   data: [
  //     {
  //       timestamp: 111111,
  //       currency: {
  //         type: CURRENCY_TYPE_MONEY,
  //         amount: 100
  //       },
  //       description: "test",
  //       type: TRANSACTION_TYPE_CHARGE,
  //       status: TRANSACTION_STATUS_SUCCEEDED,
  //       id: "1"
  //     },
  //     {
  //       timestamp: 222222,
  //       currency: {
  //         type: CURRENCY_TYPE_MONEY,
  //         amount: 100
  //       },
  //       description: "test",
  //       type: TRANSACTION_TYPE_DEPOSIT,
  //       status: TRANSACTION_STATUS_SUCCEEDED,
  //       id: "2"
  //     },
  //     {
  //       timestamp: 333333,
  //       currency: {
  //         type: CURRENCY_TYPE_MONEY,
  //         amount: 100
  //       },
  //       description: "test",
  //       type: TRANSACTION_TYPE_WITHDRAWAL,
  //       status: TRANSACTION_STATUS_SUCCEEDED,
  //       id: "3"
  //     }
  //   ],
  //   loading: false,
  //   error: null
  // };

  // const options: GamesStaticOptions = {
  //   games: [
  //     {
  //       name: GAME_LEAGUE_OF_LEGENDS,
  //       displayName: "League of Legends",
  //       disabled: true,
  //       services: []
  //     }
  //   ]
  // };

  // const addressBook: UserAddressBookState = {
  //   data: [
  //     {
  //       countryIsoCode: COUNTRY_ISO_CODE_CA,
  //       line1: "test address",
  //       city: "Montreal",
  //       id: "1"
  //     },
  //     {
  //       countryIsoCode: COUNTRY_ISO_CODE_CA,
  //       line1: "test address",
  //       city: "Quebec",
  //       id: "2"
  //     }
  //   ],
  //   loading: false,
  //   error: null
  // };

  // const email: UserEmailState = {
  //   data: { address: "gabriel@edoxa.gg", verified: true },
  //   loading: false,
  //   error: null
  // };

  // const informations: UserProfileState = {
  //   data: {
  //     firstName: "Gabriel",
  //     lastName: "Roy",
  //     gender: "Male"
  //   },
  //   loading: false,
  //   error: null
  // };

  // const phone: UserPhoneState = {
  //   data: { number: "123456789", verified: true },
  //   loading: false,
  //   error: null
  // };

  // Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <MemoryRouter>
          <Profile />
        </MemoryRouter>
      </Provider>
    )
    .toJSON();

  // Assert
  expect(tree).toMatchSnapshot();
});
