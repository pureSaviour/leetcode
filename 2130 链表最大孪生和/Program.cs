int[] nums = [5, 4, 2, 1];
ListNode head = new ListNode(nums[0]);
ListNode cur = head;
for (int i = 1; i < nums.Length; ++i)
{
    cur.next = new ListNode(nums[i]);
    cur = cur.next;
}

Solution sol = new();
var sum = sol.PairSum(head);
Console.WriteLine(sum);


public class ListNode {
    public int val;
    public ListNode next;
    public ListNode(int val=0, ListNode next=null) {
        this.val = val;
        this.next = next;
    }
}
 
public class Solution {
    public int PairSum(ListNode head) {
        ListNode slow = head, fast = head.next;
        while(fast.next is not null){
            slow = slow.next;
            fast = fast.next.next;
        }
        fast = ReverseList(slow.next);
        int sum = 0;
        while(fast is not null){
            sum = Math.Max(sum, head.val + fast.val);
            head = head.next;
            fast = fast.next;
        }
        return sum;
    }

    private static ListNode ReverseList(ListNode head){
        ListNode pre = null!;
        while(head is not null){
            var next = head.next;
            head.next = pre;
            pre = head;
            head = next;
        }
        return pre;
    }
}