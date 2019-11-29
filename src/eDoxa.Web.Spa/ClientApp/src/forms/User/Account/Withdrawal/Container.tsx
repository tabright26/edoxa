import { connect, MapStateToProps } from "react-redux";
import Withdrawal from "./Withdrawal";
import { RootState } from "store/types";
import { Bundle, Currency } from "types";

interface OwnProps {
  currency: Currency;
  bundles: Bundle[];
}

interface StateProps {
  initialValues: {
    bundle: number;
  };
}

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
  state,
  ownProps
) => {
  return {
    initialValues: {
      bundle: ownProps.bundles[0].amount
    }
  };
};

export default connect(mapStateToProps)(Withdrawal);
