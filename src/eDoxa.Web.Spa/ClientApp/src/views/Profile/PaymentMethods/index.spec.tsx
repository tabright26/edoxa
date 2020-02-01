// import React from "react";
// import renderer from "react-test-renderer";
// import PaymentMethods from "./PaymentMethods";
// import { Provider } from "react-redux";
// import { StripeBankAccountState } from "store/root/payment/stripe/bankAccount/types";
// import { StripePaymentMethodsState } from "store/root/payment/stripe/paymentMethods/types";
// import { CREATE_STRIPE_PAYMENTMETHOD_MODAL } from "modals";

it("renders without crashing", () => {
  // // Arrange
  // const paymentMethods: StripePaymentMethodsState = {
  //   data: [
  //     {
  //       id: "0",
  //       type: "card",
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
  //     country: "CA",
  //     currency: "CAD",
  //     last4: "4242",
  //     status: "verified",
  //     defaultForCurrency: true
  //   },
  //   loading: false,
  //   error: null
  // };

  // const store: any = {
  //   getState: () => {
  //     return {
  //       modal: {
  //         CREATE_STRIPE_PAYMENTMETHOD_MODAL
  //       },
  //       root: {
  //         payment: {
  //           stripe: {
  //             bankAccount,
  //             paymentMethods
  //           }
  //         }
  //       }
  //     };
  //   },
  //   dispatch: action => {},
  //   subscribe: () => {}
  // };

  // // Act
  // const tree = renderer
  //   .create(
  //     <Provider store={store}>
  //       <PaymentMethods />
  //     </Provider>
  //   )
  //   .toJSON();

  // // Assert
  // expect(tree).toMatchSnapshot();
});
