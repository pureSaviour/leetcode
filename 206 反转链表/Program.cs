
public class ListNode {
    public int val;
    public ListNode next;
    public ListNode(int val=0, ListNode next=null) {
        this.val = val;
        this.next = next;
    }
}
 
public class Solution {
    public ListNode ReverseList(ListNode head) {
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