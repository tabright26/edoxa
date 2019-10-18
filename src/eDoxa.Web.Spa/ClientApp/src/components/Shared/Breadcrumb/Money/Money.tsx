import React, { FunctionComponent, useState } from "react";
import { Badge, Popover, PopoverBody, PopoverHeader } from "reactstrap";
import Balance from "components/User/Account/Balance";
import DepositMoney from "./Deposit";
import WithdrawalMoney from "./Withdrawal";
import { MONEY } from "types";

const MoneyBreadcrumb: FunctionComponent<any> = ({ className }) => {
  const [open, setOpen] = useState(false);
  return (
    <div className={className}>
      <Badge id="moneyPopover" color="dark" style={{ width: "100px" }}>
        <Balance currency="money" attribute="available" />
      </Badge>
      <Popover
        style={{
          width: "175px"
        }}
        placement="bottom"
        isOpen={open}
        target="moneyPopover"
        trigger="hover"
        delay={{ show: 0, hide: 250 }}
        toggle={() => setOpen(!open)}
      >
        <PopoverHeader>MONEY</PopoverHeader>
        <PopoverBody>
          <DepositMoney currency={MONEY} />
          <WithdrawalMoney currency={MONEY} />
        </PopoverBody>
      </Popover>
    </div>
  );
};

export default MoneyBreadcrumb;
