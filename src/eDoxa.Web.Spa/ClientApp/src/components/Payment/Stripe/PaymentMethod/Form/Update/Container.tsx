import { connect } from "react-redux";
import { RootState } from "store/types";
import { updateStripePaymentMethod } from "store/actions/payment";
import {
  UPDATE_STRIPE_PAYMENTMETHOD_FAIL,
  StripePaymentMethodsActions
} from "store/actions/payment/types";
import Update from "./Update";
import { throwSubmissionError } from "utils/form/types";

const mapStateToProps = (state: RootState, ownProps: any) => {
  const { data } = state.root.payment.stripe.paymentMethods;
  const paymentMethod = data.find(
    paymentMethod => paymentMethod.id === ownProps.paymentMethodId
  );
  return {
    initialValues: paymentMethod
  };
};

const mapDispatchToProps = (dispatch: any, ownProps: any) => {
  return {
    updateStripePaymentMethod: (data: any) =>
      dispatch(
        updateStripePaymentMethod(
          ownProps.paymentMethodId,
          data.card.exp_month,
          data.card.exp_year
        )
      ).then((action: StripePaymentMethodsActions) => {
        switch (action.type) {
          case UPDATE_STRIPE_PAYMENTMETHOD_FAIL: {
            throwSubmissionError(action.error);
            break;
          }
        }
      })
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(Update);
