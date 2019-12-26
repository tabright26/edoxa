import { connect } from "react-redux";
import { injectStripe } from "react-stripe-elements";
import { attachStripePaymentMethod } from "store/actions/payment";
import {
  ATTACH_STRIPE_PAYMENTMETHOD_FAIL,
  StripePaymentMethodsActions
} from "store/actions/payment/types";
import Create from "./Create";
import { compose } from "recompose";
import { throwSubmissionError } from "utils/form/types";

const mapDispatchToProps = (dispatch: any, ownProps: any) => {
  return {
    createStripePaymentMethod: () =>
      ownProps.stripe.createPaymentMethod(ownProps.type).then(result => {
        if (result.paymentMethod) {
          return dispatch(
            attachStripePaymentMethod(result.paymentMethod.id)
          ).then((action: StripePaymentMethodsActions) => {
            switch (action.type) {
              case ATTACH_STRIPE_PAYMENTMETHOD_FAIL: {
                throwSubmissionError(action.error);
                break;
              }
            }
          });
        } else {
          return Promise.reject(result.error);
        }
      })
  };
};

const enhance = compose<any, any>(
  injectStripe,
  connect(null, mapDispatchToProps)
);

export default enhance(Create);
