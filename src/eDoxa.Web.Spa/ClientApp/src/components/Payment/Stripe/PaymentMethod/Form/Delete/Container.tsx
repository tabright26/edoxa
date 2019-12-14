import { connect } from "react-redux";
import { detachStripePaymentMethod } from "store/actions/payment/actions";
import {
  DETACH_STRIPE_PAYMENTMETHOD_FAIL,
  StripePaymentMethodsActions
} from "store/actions/payment/types";
import Delete from "./Delete";
import { throwSubmissionError } from "utils/form/types";

const mapDispatchToProps = (dispatch: any, ownProps: any) => {
  return {
    deleteStripePaymentMethod: () =>
      dispatch(detachStripePaymentMethod(ownProps.paymentMethodId)).then(
        (action: StripePaymentMethodsActions) => {
          switch (action.type) {
            case DETACH_STRIPE_PAYMENTMETHOD_FAIL: {
              throwSubmissionError(action.error);
              break;
            }
          }
        }
      )
  };
};

export default connect(null, mapDispatchToProps)(Delete);
