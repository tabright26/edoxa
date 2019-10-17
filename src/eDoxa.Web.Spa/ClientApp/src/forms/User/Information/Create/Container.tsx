import { connect } from "react-redux";
import { loadUserInformations, createUserInformations } from "store/root/user/informations/actions";
import Create from "./Create";

const mapDispatchToProps = (dispatch: any) => {
  return {
    createUserInformations: (data: any) =>
      dispatch(
        createUserInformations({
          ...data,
          birthDate: {
            year: data.birthDate.year,
            month: data.birthDate.month - 1,
            day: data.birthDate.day
          }
        })
      ).then(() => dispatch(loadUserInformations()))
  };
};

export default connect(
  null,
  mapDispatchToProps
)(Create);
