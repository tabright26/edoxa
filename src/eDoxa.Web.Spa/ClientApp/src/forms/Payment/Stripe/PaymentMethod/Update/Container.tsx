import { connect } from "react-redux";
import { RootState } from "store/types";
import { loadStripePaymentMethods, updateStripePaymentMethod } from "store/root/payment/stripe/paymentMethods/actions";
import { UPDATE_STRIPE_PAYMENTMETHOD_FAIL, StripePaymentMethodsActions } from "store/root/payment/stripe/paymentMethods/types";
import Update from "./Update";
import { throwSubmissionError } from "utils/form/types";

const mapStateToProps = (state: RootState, ownProps: any) => {
  const {
    data: { data }
  } = state.root.payment.stripe.paymentMethods;
  const paymentMethod = data.find(paymentMethod => paymentMethod.id === ownProps.paymentMethodId);
  return {
    initialValues: {
      card: {
        brand: paymentMethod.card.brand,
        last4: paymentMethod.card.last4,
        exp_month: paymentMethod.card.exp_month,
        exp_year: paymentMethod.card.exp_year
      }
    }
  };
};

const mapDispatchToProps = (dispatch: any, ownProps: any) => {
  return {
    updateStripePaymentMethod: (data: any) =>
      dispatch(updateStripePaymentMethod(ownProps.paymentMethodId, data.card.exp_month, data.card.exp_year)).then((action: StripePaymentMethodsActions) => {
        switch (action.type) {
          case UPDATE_STRIPE_PAYMENTMETHOD_FAIL: {
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
  mapStateToProps,
  mapDispatchToProps
)(Update);
