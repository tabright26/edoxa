import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";
import Profile from ".";
import { StripeBankAccountState } from "store/root/payment/stripe/bankAccount/types";
import { StripePaymentMethodsState } from "store/root/payment/stripe/paymentMethod/types";
import { UserTransactionState } from "store/root/user/transactionHistory/types";
import { GamesState } from "store/root/game/types";
import { UserAddressBookState } from "store/root/user/addressBook/types";
import { UserEmailState } from "store/root/user/email/types";
import { UserProfileState } from "store/root/user/profile/types";
import { UserPhoneState } from "store/root/user/phone/types";
import {
  GAME_LEAGUE_OF_LEGENDS,
  Game,
  GameOption,
  GameServiceName,
  TRANSACTION_STATUS_SUCCEEDED,
  TRANSACTION_TYPE_WITHDRAWAL,
  TRANSACTION_TYPE_DEPOSIT,
  TRANSACTION_TYPE_CHARGE,
  CURRENCY_MONEY
} from "types";

it("renders without crashing", () => {
  //Arrange
  const paymentMethods: StripePaymentMethodsState = {
    data: [
      {
        id: "0",
        type: "card",
        card: {
          brand: "visa",
          country: "CA",
          expMonth: 6,
          expYear: 2015,
          last4: "4242"
        }
      }
    ],
    loading: false,
    error: null
  };

  const bankAccount: StripeBankAccountState = {
    data: {
      bankName: "RBC",
      country: "Canada",
      currency: "CAD",
      last4: "4242",
      status: "verified",
      defaultForCurrency: true
    },
    loading: false,
    error: null
  };

  const transactions: UserTransactionState = {
    data: [
      {
        timestamp: 111111,
        currency: CURRENCY_MONEY,
        amount: 100,
        description: "test",
        type: TRANSACTION_TYPE_CHARGE,
        status: TRANSACTION_STATUS_SUCCEEDED,
        id: "1"
      },
      {
        timestamp: 222222,
        currency: CURRENCY_MONEY,
        amount: 100,
        description: "test",
        type: TRANSACTION_TYPE_DEPOSIT,
        status: TRANSACTION_STATUS_SUCCEEDED,
        id: "2"
      },
      {
        timestamp: 333333,
        currency: CURRENCY_MONEY,
        amount: 100,
        description: "test",
        type: TRANSACTION_TYPE_WITHDRAWAL,
        status: TRANSACTION_STATUS_SUCCEEDED,
        id: "3"
      }
    ],
    loading: false,
    error: null
  };

  const games: GamesState = {
    data: new Map<Game, GameOption>(),
    loading: false,
    error: null
  };

  games.data.set(GAME_LEAGUE_OF_LEGENDS, {
    name: GAME_LEAGUE_OF_LEGENDS,
    displayName: "League of Legends",
    displayed: true,
    verified: true,
    instructions: null,
    services: new Map<GameServiceName, boolean>()
  });

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
