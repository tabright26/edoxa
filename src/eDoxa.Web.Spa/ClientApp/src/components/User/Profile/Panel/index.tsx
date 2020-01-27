import React, { useState, FunctionComponent, useEffect } from "react";
import { faEdit } from "@fortawesome/free-solid-svg-icons";
import { Card, CardHeader, CardBody } from "reactstrap";
import UserProfileForm from "components/User/Profile/Form";
import { compose } from "recompose";
import Button from "components/Shared/Button";
import { Loading } from "components/Shared/Loading";
import { connect } from "react-redux";
import { RootState } from "store/types";
import { loadUserProfile } from "store/actions/identity";
import { withUserProfileDob } from "utils/oidc/containers";
import Moment from "react-moment";

const Profile: FunctionComponent<any> = ({
  className,
  profile: { data, loading },
  loadUserProfile,
  dob
}) => {
  console.log(dob);
  useEffect((): void => {
    if (data === null) {
      loadUserProfile();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  const [buttonDisabled, setbuttonDisabled] = useState(false);
  return (
    <Card className={`card-accent-primary ${className}`}>
      <CardHeader className="d-flex">
        <strong className="text-uppercase my-auto">PERSONAL INFORMATION</strong>
        <Button.Link
          className="p-0 ml-auto my-auto"
          icon={faEdit}
          disabled={buttonDisabled || !data}
          onClick={() => setbuttonDisabled(true)}
        >
          UPDATE
        </Button.Link>
      </CardHeader>
      <CardBody>
        {loading ? (
          <Loading />
        ) : !data ? (
          <UserProfileForm.Create
            handleCancel={() => setbuttonDisabled(false)}
          />
        ) : !buttonDisabled ? (
          <dl className="row mb-0">
            <dd className="col-sm-3 text-muted">Name</dd>
            <dd className="col-sm-9">
              {data.firstName} {data.lastName}
            </dd>
            <dd className="col-sm-3 text-muted">Gender</dd>
            <dd className="col-sm-9">{data.gender}</dd>
            <dd className="col-sm-3 mb-0 text-muted">Date of birth</dd>
            <dd className="col-sm-9 mb-0">
              <Moment date={[dob.year, dob.month - 1, dob.day]} format="ll" />
            </dd>
          </dl>
        ) : (
          <UserProfileForm.Update
            handleCancel={() => setbuttonDisabled(false)}
          />
        )}
      </CardBody>
    </Card>
  );
};

const mapStateToProps = (state: RootState) => {
  return {
    profile: state.root.user.profile
  };
};

const mapDispatchToProps = dispatch => {
  return {
    loadUserProfile: () => dispatch(loadUserProfile())
  };
};

const enhance = compose<any, any>(
  withUserProfileDob,
  connect(mapStateToProps, mapDispatchToProps)
);

export default enhance(Profile);
