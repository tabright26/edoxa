import { connect } from "react-redux";
import { loadStripePaymentMethods, detachStripePaymentMethod } from "store/root/payment/stripe/paymentMethods/actions";
import { DETACH_STRIPE_PAYMENTMETHOD_FAIL, StripePaymentMethodsActions } from "store/root/payment/stripe/paymentMethods/types";
import Delete from "./Delete";
import { throwSubmissionError } from "utils/form/types";

const mapDispatchToProps = (dispatch: any, ownProps: any) => {
  return {
    deleteStripePaymentMethod: () =>
      dispatch(detachStripePaymentMethod(ownProps.paymentMethodId)).then((action: StripePaymentMethodsActions) => {
        switch (action.type) {
          case DETACH_STRIPE_PAYMENTMETHOD_FAIL: {
            throwSubmissionError(action.error);
            break;
          }
          default: {
            dispatch(loadStripePaymentMethods(ownProps.type));
            break;
          }
        }
      })
  };
};

export default connect(
  null,
  mapDispatchToProps
)(Delete);
